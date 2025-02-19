﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using ATL.AudioData;
using System.Collections.Generic;

namespace ATL.test.IO.MetaData
{
    [TestClass]
    public class WAV : MetaIOTest
    {
        readonly private string notEmptyFile_bext = "WAV/broadcastwave_bext.wav";
        readonly private string notEmptyFile_info = "WAV/broadcastwave_bext_info.wav";
        readonly private string notEmptyFile_ixml = "WAV/broadcastwave_bext_iXML.wav";
        readonly private string notEmptyFile_sample = "WAV/broadcastwave_bext_iXML.wav";
        readonly private string notEmptyFile_cue = "WAV/cue.wav";

        public WAV()
        {
            emptyFile = "WAV/empty.wav";

            tagType = MetaDataIOFactory.TagType.NATIVE;
            supportsInternationalChars = false;
        }

        private void initBextTestData()
        {
            notEmptyFile = notEmptyFile_bext;

            testData = new TagData();

            testData.GeneralDescription = "bext.description";

            testData.AdditionalFields = new List<MetaFieldInfo>();
            testData.AdditionalFields.Add(new MetaFieldInfo(MetaDataIOFactory.TagType.ANY, "bext.originator", "bext.originator"));
            testData.AdditionalFields.Add(new MetaFieldInfo(MetaDataIOFactory.TagType.ANY, "bext.originatorReference", "bext.originatorReference"));
            testData.AdditionalFields.Add(new MetaFieldInfo(MetaDataIOFactory.TagType.ANY, "bext.originationDate", "2018-01-09"));
            testData.AdditionalFields.Add(new MetaFieldInfo(MetaDataIOFactory.TagType.ANY, "bext.originationTime", "01:23:45"));
            testData.AdditionalFields.Add(new MetaFieldInfo(MetaDataIOFactory.TagType.ANY, "bext.timeReference", "110801250"));
            testData.AdditionalFields.Add(new MetaFieldInfo(MetaDataIOFactory.TagType.ANY, "bext.version", "2"));
            testData.AdditionalFields.Add(new MetaFieldInfo(MetaDataIOFactory.TagType.ANY, "bext.UMID", "060A2B3401010101010102101300000000000000000000800000000000000000"));
            testData.AdditionalFields.Add(new MetaFieldInfo(MetaDataIOFactory.TagType.ANY, "bext.loudnessValue", (1.23).ToString()));
            testData.AdditionalFields.Add(new MetaFieldInfo(MetaDataIOFactory.TagType.ANY, "bext.loudnessRange", (4.56).ToString()));
            testData.AdditionalFields.Add(new MetaFieldInfo(MetaDataIOFactory.TagType.ANY, "bext.maxTruePeakLevel", (7.89).ToString()));
            testData.AdditionalFields.Add(new MetaFieldInfo(MetaDataIOFactory.TagType.ANY, "bext.maxMomentaryLoudness", (3.33).ToString()));
            testData.AdditionalFields.Add(new MetaFieldInfo(MetaDataIOFactory.TagType.ANY, "bext.maxShortTermLoudness", (-3.33).ToString()));
            testData.AdditionalFields.Add(new MetaFieldInfo(MetaDataIOFactory.TagType.ANY, "bext.codingHistory", "A=MPEG1L3,F=22050,B=56,W=20,M=dual-mono,T=haha"));
        }

        private void initListInfoTestData()
        {
            notEmptyFile = notEmptyFile_info;

            testData = new TagData();

            testData.Artist = "info.IART";
            testData.Title = "info.INAM";
            testData.Copyright = "info.ICOP";
            testData.Genre = "info.IGNR";
            testData.Comment = "info.ICMT";

            testData.AdditionalFields = new List<MetaFieldInfo>();
            testData.AdditionalFields.Add(new MetaFieldInfo(MetaDataIOFactory.TagType.ANY, "info.IARL", "info.IARL"));
            testData.AdditionalFields.Add(new MetaFieldInfo(MetaDataIOFactory.TagType.ANY, "info.ICMS", "info.ICMS"));
            testData.AdditionalFields.Add(new MetaFieldInfo(MetaDataIOFactory.TagType.ANY, "info.ICRD", "2018-01-09 01:23:45"));
            testData.AdditionalFields.Add(new MetaFieldInfo(MetaDataIOFactory.TagType.ANY, "info.IENG", "info.IENG"));
            testData.AdditionalFields.Add(new MetaFieldInfo(MetaDataIOFactory.TagType.ANY, "info.IKEY", "info.IKEY"));
            testData.AdditionalFields.Add(new MetaFieldInfo(MetaDataIOFactory.TagType.ANY, "info.IMED", "info.IMED"));
            testData.AdditionalFields.Add(new MetaFieldInfo(MetaDataIOFactory.TagType.ANY, "info.IPRD", "info.IPRD"));
            testData.AdditionalFields.Add(new MetaFieldInfo(MetaDataIOFactory.TagType.ANY, "info.ISBJ", "info.ISBJ"));
            testData.AdditionalFields.Add(new MetaFieldInfo(MetaDataIOFactory.TagType.ANY, "info.ISFT", "info.ISFT"));
            testData.AdditionalFields.Add(new MetaFieldInfo(MetaDataIOFactory.TagType.ANY, "info.ISRC", "info.ISRC"));
            testData.AdditionalFields.Add(new MetaFieldInfo(MetaDataIOFactory.TagType.ANY, "info.ISRF", "info.ISRF"));
            testData.AdditionalFields.Add(new MetaFieldInfo(MetaDataIOFactory.TagType.ANY, "info.ITCH", "info.ITCH"));
        }

        private void initDispTestData()
        {
            notEmptyFile = emptyFile;

            testData = new TagData();

            testData.AdditionalFields = new List<MetaFieldInfo>();
            testData.AdditionalFields.Add(new MetaFieldInfo(MetaDataIOFactory.TagType.ANY, "disp[0].type", "CF_TEXT"));
            testData.AdditionalFields.Add(new MetaFieldInfo(MetaDataIOFactory.TagType.ANY, "disp[0].value", "blah"));
            testData.AdditionalFields.Add(new MetaFieldInfo(MetaDataIOFactory.TagType.ANY, "disp[1].type", "CF_BITMAP"));
            testData.AdditionalFields.Add(new MetaFieldInfo(MetaDataIOFactory.TagType.ANY, "disp[1].value", "YmxhaCBibGFo"));
            testData.AdditionalFields.Add(new MetaFieldInfo(MetaDataIOFactory.TagType.ANY, "disp[2].type", "CF_METAFILE"));
            testData.AdditionalFields.Add(new MetaFieldInfo(MetaDataIOFactory.TagType.ANY, "disp[2].value", "YmxlaCBibGVo"));
            testData.AdditionalFields.Add(new MetaFieldInfo(MetaDataIOFactory.TagType.ANY, "disp[3].type", "CF_DIB"));
            testData.AdditionalFields.Add(new MetaFieldInfo(MetaDataIOFactory.TagType.ANY, "disp[3].value", "Ymx1aCBibHVo"));
            testData.AdditionalFields.Add(new MetaFieldInfo(MetaDataIOFactory.TagType.ANY, "disp[4].type", "CF_PALETTE"));
            testData.AdditionalFields.Add(new MetaFieldInfo(MetaDataIOFactory.TagType.ANY, "disp[4].value", "YmzDvGggYmzDvGg="));
        }

        private void initIXmlTestData()
        {
            notEmptyFile = notEmptyFile_ixml;

            testData = new TagData();

            testData.AdditionalFields = new List<MetaFieldInfo>();
            testData.AdditionalFields.Add(new MetaFieldInfo(MetaDataIOFactory.TagType.ANY, "ixml.PROJECT", "ANewMovie"));
            testData.AdditionalFields.Add(new MetaFieldInfo(MetaDataIOFactory.TagType.ANY, "ixml.SPEED.NOTE", "camera overcranked"));
            testData.AdditionalFields.Add(new MetaFieldInfo(MetaDataIOFactory.TagType.ANY, "ixml.SYNC_POINT_LIST.SYNC_POINT[1].SYNC_POINT_FUNCTION", "SLATE_GENERIC"));
            testData.AdditionalFields.Add(new MetaFieldInfo(MetaDataIOFactory.TagType.ANY, "ixml.TRACK_LIST.TRACK[1].NAME", "Mid"));
            testData.AdditionalFields.Add(new MetaFieldInfo(MetaDataIOFactory.TagType.ANY, "ixml.TRACK_LIST.TRACK[2].NAME", "Side"));
        }

        private void initSampleTestData()
        {
            notEmptyFile = notEmptyFile_sample;

            testData = new TagData();

            testData.AdditionalFields = new List<MetaFieldInfo>();
            testData.AdditionalFields.Add(new MetaFieldInfo(MetaDataIOFactory.TagType.ANY, "sample.manufacturer", "1"));
            testData.AdditionalFields.Add(new MetaFieldInfo(MetaDataIOFactory.TagType.ANY, "sample.product", "2"));
            testData.AdditionalFields.Add(new MetaFieldInfo(MetaDataIOFactory.TagType.ANY, "sample.period", "3"));
            testData.AdditionalFields.Add(new MetaFieldInfo(MetaDataIOFactory.TagType.ANY, "sample.MIDIUnityNote", "4"));
            testData.AdditionalFields.Add(new MetaFieldInfo(MetaDataIOFactory.TagType.ANY, "sample.MIDIPitchFraction", "5"));
            testData.AdditionalFields.Add(new MetaFieldInfo(MetaDataIOFactory.TagType.ANY, "sample.SMPTEFormat", "24"));
            testData.AdditionalFields.Add(new MetaFieldInfo(MetaDataIOFactory.TagType.ANY, "sample.SMPTEOffset.Hours", "-1"));
            testData.AdditionalFields.Add(new MetaFieldInfo(MetaDataIOFactory.TagType.ANY, "sample.SMPTEOffset.Minutes", "10"));
            testData.AdditionalFields.Add(new MetaFieldInfo(MetaDataIOFactory.TagType.ANY, "sample.SMPTEOffset.Seconds", "20"));
            testData.AdditionalFields.Add(new MetaFieldInfo(MetaDataIOFactory.TagType.ANY, "sample.SMPTEOffset.Frames", "30"));
            testData.AdditionalFields.Add(new MetaFieldInfo(MetaDataIOFactory.TagType.ANY, "sample.SampleLoop[0].CuePointId", "11"));
            testData.AdditionalFields.Add(new MetaFieldInfo(MetaDataIOFactory.TagType.ANY, "sample.SampleLoop[0].Type", "1"));
            testData.AdditionalFields.Add(new MetaFieldInfo(MetaDataIOFactory.TagType.ANY, "sample.SampleLoop[0].Start", "123"));
            testData.AdditionalFields.Add(new MetaFieldInfo(MetaDataIOFactory.TagType.ANY, "sample.SampleLoop[0].End", "456"));
            testData.AdditionalFields.Add(new MetaFieldInfo(MetaDataIOFactory.TagType.ANY, "sample.SampleLoop[0].Fraction", "8"));
            testData.AdditionalFields.Add(new MetaFieldInfo(MetaDataIOFactory.TagType.ANY, "sample.SampleLoop[0].PlayCount", "2"));
        }

        private void initCueTestReadData()
        {
            notEmptyFile = notEmptyFile_cue;

            testData = new TagData();

            testData.AdditionalFields = new List<MetaFieldInfo>();
            testData.AdditionalFields.Add(new MetaFieldInfo(MetaDataIOFactory.TagType.ANY, "cue.NumCuePoints", "10"));
            testData.AdditionalFields.Add(new MetaFieldInfo(MetaDataIOFactory.TagType.ANY, "cue.CuePoints[0].CuePointId", "1"));
            testData.AdditionalFields.Add(new MetaFieldInfo(MetaDataIOFactory.TagType.ANY, "cue.CuePoints[0].Position", "88200"));
            testData.AdditionalFields.Add(new MetaFieldInfo(MetaDataIOFactory.TagType.ANY, "cue.CuePoints[0].DataChunkId", "data"));
            testData.AdditionalFields.Add(new MetaFieldInfo(MetaDataIOFactory.TagType.ANY, "cue.CuePoints[0].ChunkStart", "0"));
            testData.AdditionalFields.Add(new MetaFieldInfo(MetaDataIOFactory.TagType.ANY, "cue.CuePoints[0].BlockStart", "0"));
            testData.AdditionalFields.Add(new MetaFieldInfo(MetaDataIOFactory.TagType.ANY, "cue.CuePoints[0].SampleOffset", "88200"));
            testData.AdditionalFields.Add(new MetaFieldInfo(MetaDataIOFactory.TagType.ANY, "cue.CuePoints[9].CuePointId", "10"));
            testData.AdditionalFields.Add(new MetaFieldInfo(MetaDataIOFactory.TagType.ANY, "cue.CuePoints[9].Position", "1730925"));
            testData.AdditionalFields.Add(new MetaFieldInfo(MetaDataIOFactory.TagType.ANY, "cue.CuePoints[9].DataChunkId", "data"));
            testData.AdditionalFields.Add(new MetaFieldInfo(MetaDataIOFactory.TagType.ANY, "cue.CuePoints[9].ChunkStart", "0"));
            testData.AdditionalFields.Add(new MetaFieldInfo(MetaDataIOFactory.TagType.ANY, "cue.CuePoints[9].BlockStart", "0"));
            testData.AdditionalFields.Add(new MetaFieldInfo(MetaDataIOFactory.TagType.ANY, "cue.CuePoints[9].SampleOffset", "1730925"));

            testData.AdditionalFields.Add(new MetaFieldInfo(MetaDataIOFactory.TagType.ANY, "info.Labels[0].Type", "labl"));
            testData.AdditionalFields.Add(new MetaFieldInfo(MetaDataIOFactory.TagType.ANY, "info.Labels[0].CuePointId", "1"));
            testData.AdditionalFields.Add(new MetaFieldInfo(MetaDataIOFactory.TagType.ANY, "info.Labels[0].Text", "MARKEURRRR 1"));
            testData.AdditionalFields.Add(new MetaFieldInfo(MetaDataIOFactory.TagType.ANY, "info.Labels[9].Type", "labl"));
            testData.AdditionalFields.Add(new MetaFieldInfo(MetaDataIOFactory.TagType.ANY, "info.Labels[9].CuePointId", "10"));
            testData.AdditionalFields.Add(new MetaFieldInfo(MetaDataIOFactory.TagType.ANY, "info.Labels[9].Text", "MARKEURRRR 8"));
        }

        private void initCueTestRWData()
        {
            notEmptyFile = notEmptyFile_cue;

            testData = new TagData();

            testData.AdditionalFields = new List<MetaFieldInfo>();
            testData.AdditionalFields.Add(new MetaFieldInfo(MetaDataIOFactory.TagType.ANY, "cue.CuePoints[0].CuePointId", "1"));
            testData.AdditionalFields.Add(new MetaFieldInfo(MetaDataIOFactory.TagType.ANY, "cue.CuePoints[0].Position", "88200"));
            testData.AdditionalFields.Add(new MetaFieldInfo(MetaDataIOFactory.TagType.ANY, "cue.CuePoints[0].DataChunkId", "data"));
            testData.AdditionalFields.Add(new MetaFieldInfo(MetaDataIOFactory.TagType.ANY, "cue.CuePoints[0].ChunkStart", "0"));
            testData.AdditionalFields.Add(new MetaFieldInfo(MetaDataIOFactory.TagType.ANY, "cue.CuePoints[0].BlockStart", "0"));
            testData.AdditionalFields.Add(new MetaFieldInfo(MetaDataIOFactory.TagType.ANY, "cue.CuePoints[0].SampleOffset", "88200"));
            testData.AdditionalFields.Add(new MetaFieldInfo(MetaDataIOFactory.TagType.ANY, "cue.CuePoints[1].CuePointId", "10"));
            testData.AdditionalFields.Add(new MetaFieldInfo(MetaDataIOFactory.TagType.ANY, "cue.CuePoints[1].Position", "1730925"));
            testData.AdditionalFields.Add(new MetaFieldInfo(MetaDataIOFactory.TagType.ANY, "cue.CuePoints[1].DataChunkId", "data"));
            testData.AdditionalFields.Add(new MetaFieldInfo(MetaDataIOFactory.TagType.ANY, "cue.CuePoints[1].ChunkStart", "0"));
            testData.AdditionalFields.Add(new MetaFieldInfo(MetaDataIOFactory.TagType.ANY, "cue.CuePoints[1].BlockStart", "0"));
            testData.AdditionalFields.Add(new MetaFieldInfo(MetaDataIOFactory.TagType.ANY, "cue.CuePoints[1].SampleOffset", "1730925"));

            testData.AdditionalFields.Add(new MetaFieldInfo(MetaDataIOFactory.TagType.ANY, "info.Labels[0].Type", "labl"));
            testData.AdditionalFields.Add(new MetaFieldInfo(MetaDataIOFactory.TagType.ANY, "info.Labels[0].CuePointId", "1"));
            testData.AdditionalFields.Add(new MetaFieldInfo(MetaDataIOFactory.TagType.ANY, "info.Labels[0].Text", "MARKEURRRR 1"));
            
            testData.AdditionalFields.Add(new MetaFieldInfo(MetaDataIOFactory.TagType.ANY, "info.Labels[1].Type", "note"));
            testData.AdditionalFields.Add(new MetaFieldInfo(MetaDataIOFactory.TagType.ANY, "info.Labels[1].CuePointId", "10"));
            testData.AdditionalFields.Add(new MetaFieldInfo(MetaDataIOFactory.TagType.ANY, "info.Labels[1].Text", "MARKEURRRR 8"));

            testData.AdditionalFields.Add(new MetaFieldInfo(MetaDataIOFactory.TagType.ANY, "info.Labels[2].Type", "ltxt"));
            testData.AdditionalFields.Add(new MetaFieldInfo(MetaDataIOFactory.TagType.ANY, "info.Labels[2].CuePointId", "11"));
            testData.AdditionalFields.Add(new MetaFieldInfo(MetaDataIOFactory.TagType.ANY, "info.Labels[2].SampleLength", "1234"));
            testData.AdditionalFields.Add(new MetaFieldInfo(MetaDataIOFactory.TagType.ANY, "info.Labels[2].PurposeId", "5678"));
            testData.AdditionalFields.Add(new MetaFieldInfo(MetaDataIOFactory.TagType.ANY, "info.Labels[2].Country", "2"));
            testData.AdditionalFields.Add(new MetaFieldInfo(MetaDataIOFactory.TagType.ANY, "info.Labels[2].Language", "4"));
            testData.AdditionalFields.Add(new MetaFieldInfo(MetaDataIOFactory.TagType.ANY, "info.Labels[2].Dialect", "6"));
            testData.AdditionalFields.Add(new MetaFieldInfo(MetaDataIOFactory.TagType.ANY, "info.Labels[2].CodePage", "8"));
            testData.AdditionalFields.Add(new MetaFieldInfo(MetaDataIOFactory.TagType.ANY, "info.Labels[2].Text", "HEYHEY 10"));
        }

        [TestMethod]
        public void TagIO_R_WAV_BEXT_simple()
        {
            new ConsoleLogger();
            initBextTestData();

            string location = TestUtils.GetResourceLocationRoot() + notEmptyFile;
            AudioDataManager theFile = new AudioDataManager(AudioDataIOFactory.GetInstance().GetFromPath(location));

            readExistingTagsOnFile(theFile, 0);
        }

        [TestMethod]
        public void TagIO_R_WAV_INFO_simple()
        {
            new ConsoleLogger();
            initListInfoTestData();

            string location = TestUtils.GetResourceLocationRoot() + notEmptyFile;
            AudioDataManager theFile = new AudioDataManager(AudioDataIOFactory.GetInstance().GetFromPath(location));

            readExistingTagsOnFile(theFile, 0);
        }

        [TestMethod]
        public void TagIO_R_WAV_IXML_simple()
        {
            new ConsoleLogger();
            initIXmlTestData();

            string location = TestUtils.GetResourceLocationRoot() + notEmptyFile;
            AudioDataManager theFile = new AudioDataManager(AudioDataIOFactory.GetInstance().GetFromPath(location));

            readExistingTagsOnFile(theFile, 0);
        }

        [TestMethod]
        public void TagIO_R_Cue_simple()
        {
            new ConsoleLogger();
            initCueTestReadData();

            string location = TestUtils.GetResourceLocationRoot() + notEmptyFile;
            AudioDataManager theFile = new AudioDataManager(AudioDataIOFactory.GetInstance().GetFromPath(location));

            readExistingTagsOnFile(theFile, 0);
        }

        [TestMethod]
        public void TagIO_RW_WAV_BEXT_Empty()
        {
            initBextTestData();
            test_RW_Empty(emptyFile, true, true, true);
        }

        [TestMethod]
        public void TagIO_RW_WAV_INFO_Empty()
        {
            initListInfoTestData();
            test_RW_Empty(emptyFile, true, true, true);
        }

        [TestMethod]
        public void TagIO_RW_WAV_IXML_Empty()
        {
            initIXmlTestData();
            test_RW_Empty(emptyFile, true, true, true);
        }

        [TestMethod]
        public void TagIO_RW_WAV_Sample_Empty()
        {
            initSampleTestData();
            test_RW_Empty(emptyFile, true, true, true);
        }

        [TestMethod]
        public void TagIO_RW_WAV_Cue_Empty()
        {
            initCueTestRWData();
            test_RW_Empty(emptyFile, true, true, true);
        }

        [TestMethod]
        public void TagIO_RW_WAV_BEXT_Existing()
        {
            initBextTestData();
            test_RW_Existing(notEmptyFile, 0, true, true, true);
        }

        [TestMethod]
        public void TagIO_RW_WAV_LIST_INFO_Existing()
        {
            initListInfoTestData();
            test_RW_Existing(notEmptyFile, 0, true, true, false); // CRC check impossible because of field order
        }

        [TestMethod]
        public void TagIO_RW_WAV_DISP_Existing()
        {
            initDispTestData();
            test_RW_Empty(emptyFile, true, true, true);
        }

        [TestMethod]
        public void TagIO_RW_WAV_IXML_Existing()
        {
            initIXmlTestData();
            test_RW_Existing(notEmptyFile, 0, true, false, false); // length-check impossible because of parasite end-of-line characters and padding
        }

        /* Should find a tool to edit all these informations manually to create a test file
        [TestMethod]
        public void TagIO_RW_WAV_Sample_Existing()
        {
            initSampleTestData();
            test_RW_Existing(notEmptyFile, 0, true, false, false); // length-check impossible because of parasite end-of-line characters and padding
        }
        */

        [TestMethod]
        public void TagIO_RW_WAV_Cue_Existing()
        {
            initCueTestRWData();
            test_RW_Existing(notEmptyFile, 0, true, false, false); // length-check impossible because of parasite end-of-line characters and padding
        }
    }
}
