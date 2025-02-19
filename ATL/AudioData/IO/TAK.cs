using ATL.Logging;
using System;
using System.IO;
using static ATL.AudioData.AudioDataManager;
using Commons;
using static ATL.ChannelsArrangements;

namespace ATL.AudioData.IO
{
    /// <summary>
    /// Class for Tom's lossless Audio Kompressor files manipulation (extension : .TAK)
    /// </summary>
	class TAK : IAudioDataIO
    {
        // Headers ID
        public const int TAK_VERSION_100 = 0;
        public const int TAK_VERSION_210 = 210;
        public const int TAK_VERSION_220 = 220;

        public const String TAK_ID = "tBaK";


        // Private declarations 
        private uint formatVersion;
        private uint bits;
        private uint sampleRate;

        private double bitrate;
        private double duration;
        private ChannelsArrangement channelsArrangement;
        private bool isValid;

        private SizeInfo sizeInfo;
        private readonly string filePath;


        // Public declarations 
        public uint Bits
        {
            get { return bits; }
        }
        public double CompressionRatio
        {
            get { return getCompressionRatio(); }
        }


        // ---------- INFORMATIVE INTERFACE IMPLEMENTATIONS & MANDATORY OVERRIDES

        public int SampleRate
        {
            get { return (int)sampleRate; }
        }
        public bool IsVBR
        {
            get { return false; }
        }
        public Format AudioFormat
        {
            get;
        }
        public int CodecFamily
        {
            get { return AudioDataIOFactory.CF_LOSSLESS; }
        }
        public string FileName
        {
            get { return filePath; }
        }
        public double BitRate
        {
            get { return bitrate; }
        }
        public double Duration
        {
            get { return duration; }
        }
        public ChannelsArrangement ChannelsArrangement
        {
            get { return channelsArrangement; }
        }
        public bool IsMetaSupported(MetaDataIOFactory.TagType metaDataType)
        {
            return metaDataType == MetaDataIOFactory.TagType.APE;
        }
        public long AudioDataOffset { get; set; }
        public long AudioDataSize { get; set; }


        // ---------- CONSTRUCTORS & INITIALIZERS

        private void resetData()
        {
            duration = 0;
            bitrate = 0;
            isValid = false;

            formatVersion = 0;
            bits = 0;
            sampleRate = 0;

            AudioDataOffset = -1;
            AudioDataSize = 0;
        }

        public TAK(string filePath, Format format)
        {
            this.filePath = filePath;
            AudioFormat = format;
            resetData();
        }


        // ---------- SUPPORT METHODS

        private double getCompressionRatio()
        {
            // Get compression ratio 
            if (isValid)
                return (double)sizeInfo.FileSize / (duration * sampleRate * (channelsArrangement.NbChannels * bits / 8) + 44) * 100;
            else
                return 0;
        }

        public bool Read(BinaryReader source, SizeInfo sizeInfo, MetaDataIO.ReadTagParams readTagParams)
        {
            bool result = false;
            bool doLoop = true;
            long position;

            UInt16 readData16;
            UInt32 readData32;

            UInt32 metaType;
            UInt32 metaSize;
            long sampleCount = 0;
            int frameSizeType = -1;

            this.sizeInfo = sizeInfo;
            resetData();
            source.BaseStream.Seek(sizeInfo.ID3v2Size, SeekOrigin.Begin);

            if (TAK_ID.Equals(Utils.Latin1Encoding.GetString(source.ReadBytes(4))))
            {
                result = true;
                AudioDataOffset = source.BaseStream.Position - 4;
                AudioDataSize = sizeInfo.FileSize - sizeInfo.APESize - sizeInfo.ID3v1Size - AudioDataOffset;

                do // Loop metadata
                {
                    readData32 = source.ReadUInt32();

                    metaType = readData32 & 0x7F;
                    metaSize = readData32 >> 8;

                    position = source.BaseStream.Position;

                    if (0 == metaType) doLoop = false; // End of metadata
                    else if (0x01 == metaType) // Stream info
                    {
                        readData16 = source.ReadUInt16();
                        frameSizeType = readData16 & 0x003C; // bits 11 to 14
                        readData32 = source.ReadUInt32();
                        uint restOfData = source.ReadUInt32();

                        sampleCount = (readData16 >> 14) + (readData32 << 2) + ((restOfData & 0x00000080) << 34);

                        sampleRate = ((restOfData >> 4) & 0x03ffff) + 6000; // bits 4 to 21
                        channelsArrangement = ChannelsArrangements.GuessFromChannelNumber((int)((restOfData >> 27) & 0x0F) + 1); // bits 28 to 31

                        if (sampleCount > 0)
                        {
                            duration = (double)sampleCount * 1000.0 / sampleRate;
                            bitrate = Math.Round(((double)(sizeInfo.FileSize - source.BaseStream.Position)) * 8 / duration); //time to calculate average bitrate
                        }
                    }
                    else if (0x04 == metaType) // Encoder info
                    {
                        readData32 = source.ReadUInt32();
                        formatVersion = 100 * ((readData32 & 0x00ff0000) >> 16);
                        formatVersion += 10 * ((readData32 & 0x0000ff00) >> 8);
                        formatVersion += (readData32 & 0x000000ff);
                    }

                    source.BaseStream.Seek(position + metaSize, SeekOrigin.Begin);
                } while (doLoop); // End of metadata loop
            }

            return result;
        }
    }
}