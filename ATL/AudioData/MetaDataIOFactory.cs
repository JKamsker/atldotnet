using System;
using System.Collections.Generic;

namespace ATL.AudioData
{
    /// <summary>
    /// Factory for metadata (tag) I/O classes
    /// </summary>
    public class MetaDataIOFactory : Factory
    {
        // Defines the types of supported "cross-format" metadata
        /// <summary>
        /// ID3v1
        /// </summary>
        [ObsoleteAttribute("Use TagType.ID3V1 instead", false)]
        public const TagType TAG_ID3V1 = TagType.ID3V1;
        /// <summary>
        /// ID3v2
        /// </summary>
        [ObsoleteAttribute("Use TagType.ID3V2 instead", false)]
        public const TagType TAG_ID3V2 = TagType.ID3V2;
        /// <summary>
        /// APEtag
        /// </summary>
        [ObsoleteAttribute("Use TagType.APE instead", false)]
        public const TagType TAG_APE = TagType.APE;
        /// <summary>
        /// Native tag format associated with the audio container (ex : MP4 built-in tagging format)
        /// </summary>
        [ObsoleteAttribute("Use TagType.NATIVE instead", false)]
        public const TagType TAG_NATIVE = TagType.NATIVE;
        /// <summary>
        /// Whenever tag type is not known in advance and may apply to any available tag
        /// </summary>
        [ObsoleteAttribute("Use TagType.ANY instead", false)]
        public const TagType TAG_ANY = TagType.ANY;

        /// <summary>
        /// Count of the types defined above, excluding "any" type
        /// </summary>
        public static readonly int TAG_TYPE_COUNT = 4;

        /// <summary>
        /// Metadata type
        /// </summary>
        public enum TagType
        {
            /// <summary>
            /// ID3v1
            /// </summary>
            ID3V1 = 0,
            /// <summary>
            /// ID3v2
            /// </summary>
            ID3V2 = 1,
            /// <summary>
            /// APEtag
            /// </summary>
            APE = 2,
            /// <summary>
            /// Native tag format associated with the audio container (ex : MP4 built-in tagging format)
            /// </summary>
            NATIVE = 3,
            /// <summary>
            /// Whenever tag type is not known in advance and may apply to any available tag
            /// </summary>
            ANY = 99
        }

        // The instance of this factory
        private static MetaDataIOFactory theFactory = null;

        private static readonly object _lockable = new object();


        /// <summary>
        /// Sets whether the next created metadatareaders should use cross-tag reading
        ///   - false           :  the most important tagging standard (according to priorities)
        ///                        detected on the track is exclusively used to populate fields
        ///   - true (default)  :  for each field, the most important tagging standard (according to
        ///                        priorities) is first read. If the value is empty, the next
        ///                        tagging standard (according to priorities) is read, and so on...
        /// </summary>
        public bool CrossReading { get; set; } = true;

        /// <summary>
        /// Ordered list of tags to consider while reading, starting by the one with the highest priority
        /// </summary>
        public TagType[] TagPriority { get; set; }

        // ------------------------------------------------------------------------------------------

        /// <summary>
        /// Gets the instance of this factory (Singleton pattern) 
        /// </summary>
        /// <returns>Instance of the MetaReaderFactory of the application</returns>
        public static MetaDataIOFactory GetInstance()
        {
            if (!BitConverter.IsLittleEndian) throw new PlatformNotSupportedException("Big-endian based platforms are not supported by ATL");
            lock (_lockable)
            {
                if (null == theFactory)
                {
                    theFactory = new MetaDataIOFactory();
                    theFactory.TagPriority = new TagType[TAG_TYPE_COUNT];
                    theFactory.TagPriority[0] = TagType.ID3V2;
                    theFactory.TagPriority[1] = TagType.APE;
                    theFactory.TagPriority[2] = TagType.NATIVE;
                    theFactory.TagPriority[3] = TagType.ID3V1;

                    theFactory.formatListByExt = new Dictionary<string, IList<Format>>();
                    theFactory.formatListByMime = new Dictionary<string, IList<Format>>();

                    Format tempFmt = new Format(((int)TagType.ID3V1) * 1000, "ID3v1");
                    tempFmt.AddExtension("id3v1");
                    theFactory.addFormat(tempFmt);

                    tempFmt = new Format(((int)TagType.ID3V2) * 1000, "ID3v2");
                    tempFmt.AddExtension("id3v2");
                    theFactory.addFormat(tempFmt);

                    tempFmt = new Format(((int)TagType.APE) * 1000, "APEtag");
                    tempFmt.AddExtension("ape");
                    theFactory.addFormat(tempFmt);

                    tempFmt = new Format(((int)TagType.NATIVE) * 1000, "Native");
                    tempFmt.AddExtension("native");
                    theFactory.addFormat(tempFmt);
                }
            }

            return theFactory;
        }


        /// <summary>
        /// Modifies the default reading priority of the metadata
        /// </summary>
        /// <param name="type">Identifier of the metadata type</param>
        /// <param name="rank">Reading priority : range [0..TAG_TYPE_COUNT[</param>
        public void SetTagPriority(TagType type, int rank)
        {
            if ((rank > -1) && (rank < TagPriority.Length))
                TagPriority[rank] = type;
        }

        /// <summary>
        /// Gets the appropriate metadata reader for a given file / physical data reader
        /// </summary>
        /// <param name="theDataManager">AudioDataReader produced for this file</param>
        /// <param name="forceTagType">Forces a certain tag type to be read regardless of the current "cross reading" settings</param>
        /// <returns>Metadata reader able to give metadata info for this file (or the dummy reader if the format is unknown)</returns>
        public IMetaDataIO GetMetaReader(AudioDataManager theDataManager, TagType forceTagType = TagType.ANY)
        {
            IMetaDataIO theMetaReader = null;

            int tagCount = 0;
            if (theDataManager.HasNativeMeta()) tagCount++;
            if (theDataManager.ID3v1.Exists) tagCount++;
            if (theDataManager.ID3v2.Exists) tagCount++;
            if (theDataManager.APEtag.Exists) tagCount++;

            if (CrossReading && (tagCount > 1) && forceTagType.Equals(TagType.ANY))
            {
                theMetaReader = new CrossMetadataReader(theDataManager, TagPriority);
            }
            else
            {
                for (int i = 0; i < TAG_TYPE_COUNT; i++)
                {
                    if (((TagType.NATIVE == TagPriority[i] && forceTagType.Equals(TagType.ANY)) || forceTagType.Equals(TagType.NATIVE)) && theDataManager.HasNativeMeta())
                    {
                        theMetaReader = theDataManager.NativeTag; break;
                    }
                    if (((TagType.ID3V1 == TagPriority[i] && forceTagType.Equals(TagType.ANY)) || forceTagType.Equals(TagType.ID3V1)) && theDataManager.ID3v1.Exists)
                    {
                        theMetaReader = theDataManager.ID3v1; break;
                    }
                    if (((TagType.ID3V2 == TagPriority[i] && forceTagType.Equals(TagType.ANY)) || forceTagType.Equals(TagType.ID3V2)) && theDataManager.ID3v2.Exists)
                    {
                        theMetaReader = theDataManager.ID3v2; break;
                    }
                    if (((TagType.APE == TagPriority[i] && forceTagType.Equals(TagType.ANY)) || forceTagType.Equals(TagType.APE)) && theDataManager.APEtag.Exists)
                    {
                        theMetaReader = theDataManager.APEtag; break;
                    }
                }
            }

            if (null == theMetaReader) theMetaReader = new IO.DummyTag();

            return theMetaReader;
        }
    }
}
