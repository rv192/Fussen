using System;
using System.Collections.Generic;
using System.Text;

namespace Fussen.Graphics
{
    public class ExifViewer : IDisposable
    {

        private System.Drawing.Bitmap _Image;
        private System.Text.Encoding _Encoding = System.Text.Encoding.UTF8;

        #region Type declarations
        // 
        // Contains possible values of EXIF tag names (ID)
        // 
        // See GdiPlusImaging.h
        // 
        // [altair] 10.09.2003 Created
        // 
        public enum TagNames : int
        {
            ExifIFD = 0x8769,
            GpsIFD = 0x8825,
            NewSubfileType = 0xFE,
            SubfileType = 0xFF,
            ImageWidth = 0x100,
            ImageHeight = 0x101,
            BitsPerSample = 0x102,
            Compression = 0x103,
            PhotometricInterp = 0x106,
            ThreshHolding = 0x107,
            CellWidth = 0x108,
            CellHeight = 0x109,
            FillOrder = 0x10A,
            DocumentName = 0x10D,
            ImageDescription = 0x10E,
            EquipMake = 0x10F,
            EquipModel = 0x110,
            StripOffsets = 0x111,
            Orientation = 0x112,
            SamplesPerPixel = 0x115,
            RowsPerStrip = 0x116,
            StripBytesCount = 0x117,
            MinSampleValue = 0x118,
            MaxSampleValue = 0x119,
            XResolution = 0x11A,
            YResolution = 0x11B,
            PlanarConfig = 0x11C,
            PageName = 0x11D,
            XPosition = 0x11E,
            YPosition = 0x11F,
            FreeOffset = 0x120,
            FreeByteCounts = 0x121,
            GrayResponseUnit = 0x122,
            GrayResponseCurve = 0x123,
            T4Option = 0x124,
            T6Option = 0x125,
            ResolutionUnit = 0x128,
            PageNumber = 0x129,
            TransferFuncition = 0x12D,
            SoftwareUsed = 0x131,
            DateTime = 0x132,
            Artist = 0x13B,
            HostComputer = 0x13C,
            Predictor = 0x13D,
            WhitePoint = 0x13E,
            PrimaryChromaticities = 0x13F,
            ColorMap = 0x140,
            HalftoneHints = 0x141,
            TileWidth = 0x142,
            TileLength = 0x143,
            TileOffset = 0x144,
            TileByteCounts = 0x145,
            InkSet = 0x14C,
            InkNames = 0x14D,
            NumberOfInks = 0x14E,
            DotRange = 0x150,
            TargetPrinter = 0x151,
            ExtraSamples = 0x152,
            SampleFormat = 0x153,
            SMinSampleValue = 0x154,
            SMaxSampleValue = 0x155,
            TransferRange = 0x156,
            JPEGProc = 0x200,
            JPEGInterFormat = 0x201,
            JPEGInterLength = 0x202,
            JPEGRestartInterval = 0x203,
            JPEGLosslessPredictors = 0x205,
            JPEGPointTransforms = 0x206,
            JPEGQTables = 0x207,
            JPEGDCTables = 0x208,
            JPEGACTables = 0x209,
            YCbCrCoefficients = 0x211,
            YCbCrSubsampling = 0x212,
            YCbCrPositioning = 0x213,
            REFBlackWhite = 0x214,
            ICCProfile = 0x8773,
            Gamma = 0x301,
            ICCProfileDescriptor = 0x302,
            SRGBRenderingIntent = 0x303,
            ImageTitle = 0x320,
            Copyright = 0x8298,
            ResolutionXUnit = 0x5001,
            ResolutionYUnit = 0x5002,
            ResolutionXLengthUnit = 0x5003,
            ResolutionYLengthUnit = 0x5004,
            PrintFlags = 0x5005,
            PrintFlagsVersion = 0x5006,
            PrintFlagsCrop = 0x5007,
            PrintFlagsBleedWidth = 0x5008,
            PrintFlagsBleedWidthScale = 0x5009,
            HalftoneLPI = 0x500A,
            HalftoneLPIUnit = 0x500B,
            HalftoneDegree = 0x500C,
            HalftoneShape = 0x500D,
            HalftoneMisc = 0x500E,
            HalftoneScreen = 0x500F,
            JPEGQuality = 0x5010,
            GridSize = 0x5011,
            ThumbnailFormat = 0x5012,
            ThumbnailWidth = 0x5013,
            ThumbnailHeight = 0x5014,
            ThumbnailColorDepth = 0x5015,
            ThumbnailPlanes = 0x5016,
            ThumbnailRawBytes = 0x5017,
            ThumbnailSize = 0x5018,
            ThumbnailCompressedSize = 0x5019,
            ColorTransferFunction = 0x501A,
            ThumbnailData = 0x501B,
            ThumbnailImageWidth = 0x5020,
            ThumbnailImageHeight = 0x502,
            ThumbnailBitsPerSample = 0x5022,
            ThumbnailCompression = 0x5023,
            ThumbnailPhotometricInterp = 0x5024,
            ThumbnailImageDescription = 0x5025,
            ThumbnailEquipMake = 0x5026,
            ThumbnailEquipModel = 0x5027,
            ThumbnailStripOffsets = 0x5028,
            ThumbnailOrientation = 0x5029,
            ThumbnailSamplesPerPixel = 0x502A,
            ThumbnailRowsPerStrip = 0x502B,
            ThumbnailStripBytesCount = 0x502C,
            ThumbnailResolutionX = 0x502D,
            ThumbnailResolutionY = 0x502E,
            ThumbnailPlanarConfig = 0x502F,
            ThumbnailResolutionUnit = 0x5030,
            ThumbnailTransferFunction = 0x5031,
            ThumbnailSoftwareUsed = 0x5032,
            ThumbnailDateTime = 0x5033,
            ThumbnailArtist = 0x5034,
            ThumbnailWhitePoint = 0x5035,
            ThumbnailPrimaryChromaticities = 0x5036,
            ThumbnailYCbCrCoefficients = 0x5037,
            ThumbnailYCbCrSubsampling = 0x5038,
            ThumbnailYCbCrPositioning = 0x5039,
            ThumbnailRefBlackWhite = 0x503A,
            ThumbnailCopyRight = 0x503B,
            LuminanceTable = 0x5090,
            ChrominanceTable = 0x5091,
            FrameDelay = 0x5100,
            LoopCount = 0x5101,
            PixelUnit = 0x5110,
            PixelPerUnitX = 0x5111,
            PixelPerUnitY = 0x5112,
            PaletteHistogram = 0x5113,
            ExifExposureTime = 0x829A,
            ExifFNumber = 0x829D,
            ExifExposureProg = 0x8822,
            ExifSpectralSense = 0x8824,
            ExifISOSpeed = 0x8827,
            ExifOECF = 0x8828,
            ExifVer = 0x9000,
            ExifDTOrig = 0x9003,
            ExifDTDigitized = 0x9004,
            ExifCompConfig = 0x9101,
            ExifCompBPP = 0x9102,
            ExifShutterSpeed = 0x9201,
            ExifAperture = 0x9202,
            ExifBrightness = 0x9203,
            ExifExposureBias = 0x9204,
            ExifMaxAperture = 0x9205,
            ExifSubjectDist = 0x9206,
            ExifMeteringMode = 0x9207,
            ExifLightSource = 0x9208,
            ExifFlash = 0x9209,
            ExifFocalLength = 0x920A,
            ExifMakerNote = 0x927C,
            ExifUserComment = 0x9286,
            ExifDTSubsec = 0x9290,
            ExifDTOrigSS = 0x9291,
            ExifDTDigSS = 0x9292,
            ExifFPXVer = 0xA000,
            ExifColorSpace = 0xA001,
            ExifPixXDim = 0xA002,
            ExifPixYDim = 0xA003,
            ExifRelatedWav = 0xA004,
            ExifInterop = 0xA005,
            ExifFlashEnergy = 0xA20B,
            ExifSpatialFR = 0xA20C,
            ExifFocalXRes = 0xA20E,
            ExifFocalYRes = 0xA20F,
            ExifFocalResUnit = 0xA210,
            ExifSubjectLoc = 0xA214,
            ExifExposureIndex = 0xA215,
            ExifSensingMethod = 0xA217,
            ExifFileSource = 0xA300,
            ExifSceneType = 0xA301,
            ExifCfaPattern = 0xA302,
            GpsVer = 0x0,
            GpsLatitudeRef = 0x1,
            GpsLatitude = 0x2,
            GpsLongitudeRef = 0x3,
            GpsLongitude = 0x4,
            GpsAltitudeRef = 0x5,
            GpsAltitude = 0x6,
            GpsGpsTime = 0x7,
            GpsGpsSatellites = 0x8,
            GpsGpsStatus = 0x9,
            GpsGpsMeasureMode = 0xA,
            GpsGpsDop = 0xB,
            GpsSpeedRef = 0xC,
            GpsSpeed = 0xD,
            GpsTrackRef = 0xE,
            GpsTrack = 0xF,
            GpsImgDirRef = 0x10,
            GpsImgDir = 0x11,
            GpsMapDatum = 0x12,
            GpsDestLatRef = 0x13,
            GpsDestLat = 0x14,
            GpsDestLongRef = 0x15,
            GpsDestLong = 0x16,
            GpsDestBearRef = 0x17,
            GpsDestBear = 0x18,
            GpsDestDistRef = 0x19,
            GpsDestDist = 0x1A
        }


        // 
        // Real position of 0th row and column of picture
        // 
        // 
        // 
        // [altair] 10.09.2003 Created
        // 

        public enum Orientations
        {
            TopLeft = 1,
            TopRight = 2,
            BottomRight = 3,
            BottomLeft = 4,
            LeftTop = 5,
            RightTop = 6,
            RightBottom = 7,
            LftBottom = 8
        }


        // 
        // Exposure programs
        // 
        // 
        // 
        // [altair] 10.09.2003 Created
        // 

        public enum ExposurePrograms
        {
            Manual = 1,
            Normal = 2,
            AperturePriority = 3,
            ShutterPriority = 4,
            Creative = 5,
            Action = 6,
            Portrait = 7,
            Landscape = 8,
        }


        // 
        // Exposure metering modes
        // 
        // 
        // 
        // [altair] 10.09.2003 Created
        // 

        public enum ExposureMeteringModes
        {
            Unknown = 0,
            Average = 1,
            CenterWeightedAverage = 2,
            Spot = 3,
            MultiSpot = 4,
            MultiSegment = 5,
            Partial = 6,
            Other = 255
        }


        // 
        // Flash activity modes
        // 
        // 
        // 
        // [altair] 10.09.2003 Created
        // 

        public enum FlashModes
        {
            NotFired = 0,
            Fired = 1,
            FiredButNoStrobeReturned = 5,
            FiredAndStrobeReturned = 7,
        }


        // 
        // Possible light sources (white balance)
        // 
        // 
        // 
        // [altair] 10.09.2003 Created
        // 

        public enum LightSources
        {
            Unknown = 0,
            Daylight = 1,
            Fluorescent = 2,
            Tungsten = 3,
            Flash = 10,
            StandardLightA = 17,
            StandardLightB = 18,
            StandardLightC = 19,
            D55 = 20,
            D65 = 21,
            D75 = 22,
            Other = 255
        }


        // 
        // EXIF data types
        // 
        // 
        // 
        // [altair] 12.6.2004 Created
        // 
        public enum ExifDataTypes : short
        {
            UnsignedByte = 1,
            AsciiString = 2,
            UnsignedShort = 3,
            UnsignedLong = 4,
            UnsignedRational = 5,
            SignedByte = 6,
            Undefined = 7,
            SignedShort = 8,
            SignedLong = 9,
            SignedRational = 10,
            SingleFloat = 11,
            DoubleFloat = 12
        }


        // 
        // Represents rational which is type of some Exif properties
        // 
        // 
        // 
        // [altair] 10.09.2003 Created
        // 
        public struct Rational
        {
            public Int32 Numerator;
            public Int32 Denominator;


            // 
            // Converts rational to string representation
            // 
            // Optional, default "/". String to be used as delimiter of components.
            // String representation of the rational.
            // 
            // 
            // [altair] 10.09.2003 Created
            // 

            public override string ToString()
            {
                return ToString("/");
            }

            public string ToString(string Delimiter)
            {
                return Numerator + "/" + Denominator;
            }

            // 
            // Converts rational to double precision real number
            // 
            // The rational as double precision real number.
            // 
            // 
            // [altair] 10.09.2003 Created
            // 

            public double ToDouble()
            {
                return (double)Numerator / Denominator;
            }
        }

        #endregion

        /// <summary>
        /// Initializes new instance of this class.
        /// </summary>
        /// <param name="Bitmap">Bitmap to read exif information from</param>
		public ExifViewer(System.Drawing.Bitmap Bitmap)
        {
            if (Bitmap == null)
                throw new ArgumentNullException("Bitmap");
            this._Image = Bitmap;
        }

        /// <summary>
        /// Initializes new instance of this class.
        /// </summary>
        /// <param name="FileName">Name of file to be loaded</param>
		public ExifViewer(string FileName)
        {
            this._Image = (System.Drawing.Bitmap)System.Drawing.Bitmap.FromFile(FileName);
        }

        /// <summary>
        /// Get or set encoding used for string metadata
        /// </summary>
        /// <remarks>
        /// Encoding used for string metadata
        /// Default encoding is UTF-8
        /// </remarks>
        public System.Text.Encoding Encoding
        {
            get
            {
                return this._Encoding;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException();
                this._Encoding = value;
            }
        }

        /// <summary>
        /// Returns copy of bitmap this instance is working on
        /// </summary>
        /// <returns></returns>
        public System.Drawing.Bitmap GetBitmap()
        {
            return (System.Drawing.Bitmap)this._Image.Clone();
        }

        /// <summary>
        /// Returns all available data in formatted string form
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            System.Text.StringBuilder SB = new StringBuilder();

            SB.Append("Image:");
            SB.Append("\n\tDimensions: " + this.Width + " x " + this.Height + " px");
            SB.Append("\n\tResolution: " + this.ResolutionX + " x " + this.ResolutionY + " dpi");
            SB.Append("\n\tOrientation: " + Enum.GetName(typeof(Orientations), this.Orientation));
            SB.Append("\n\tTitle: " + this.Title);
            SB.Append("\n\tDescription: " + this.Description);
            SB.Append("\n\tCopyright: " + this.Copyright);
            SB.Append("\nEquipment:");
            SB.Append("\n\tMaker: " + this.EquipmentMaker);
            SB.Append("\n\tModel: " + this.EquipmentModel);
            SB.Append("\n\tSoftware: " + this.Software);
            SB.Append("\nDate and time:");
            SB.Append("\n\tGeneral: " + this.DateTimeLastModified.ToString());
            SB.Append("\n\tOriginal: " + this.DateTimeOriginal.ToString());
            SB.Append("\n\tDigitized: " + this.DateTimeDigitized.ToString());
            SB.Append("\nShooting conditions:");
            SB.Append("\n\tExposure time: " + this.ExposureTime.ToString("N4") + " s");
            SB.Append("\n\tExposure program: " + Enum.GetName(typeof(ExposurePrograms), this.ExposureProgram));
            SB.Append("\n\tExposure mode: " + Enum.GetName(typeof(ExposureMeteringModes), this.ExposureMeteringMode));
            SB.Append("\n\tAperture: F" + this.Aperture.ToString("N2"));
            SB.Append("\n\tISO sensitivity: " + this.ISO);
            SB.Append("\n\tSubject distance: " + this.SubjectDistance.ToString("N2") + " m");
            SB.Append("\n\tFocal length: " + this.FocalLength);
            SB.Append("\n\tFlash: " + Enum.GetName(typeof(FlashModes), this.FlashMode));
            SB.Append("\n\tLight source (WB): " + Enum.GetName(typeof(LightSources), this.LightSource));
            //SB.Replace("\n", vbCrLf);
            //SB.Replace("\t", vbTab);
            return SB.ToString();
        }

        #region Nicely formatted well-known properties

        /// <summary>
        /// Brand of equipment (EXIF EquipMake)
        /// </summary>
        public string EquipmentMaker
        {
            get
            {
                return this.GetPropertyString((int)TagNames.EquipMake);
            }
        }

        /// <summary>
        /// Model of equipment (EXIF EquipModel)
        /// </summary>
        public string EquipmentModel
        {
            get
            {
                return this.GetPropertyString((int)TagNames.EquipModel);
            }
        }

        /// <summary>
        /// Software used for processing (EXIF Software)
        /// </summary>
        public string Software
        {
            get
            {
                return this.GetPropertyString((int)TagNames.SoftwareUsed);
            }
        }

        /// <summary>
        /// Orientation of image (position of row 0, column 0) (EXIF Orientation)
        /// </summary>
        public Orientations Orientation
        {
            get
            {
                Int32 X = this.GetPropertyInt16((int)TagNames.Orientation);

                if (!Enum.IsDefined(typeof(Orientations), X))
                    return Orientations.TopLeft;
                else
                    return (Orientations)Enum.Parse(typeof(Orientations), Enum.GetName(typeof(Orientations), X));
            }
        }

        /// <summary>
        /// Time when image was last modified (EXIF DateTime).
        /// </summary>
        public DateTime DateTimeLastModified
        {
            get
            {
                try
                {
                    return DateTime.ParseExact(this.GetPropertyString((int)TagNames.DateTime), @"yyyy\:MM\:dd HH\:mm\:ss", null);
                }
                catch
                {
                    return DateTime.MinValue;
                }
            }
            set
            {
                try
                {
                    this.SetPropertyString((int)TagNames.DateTime, value.ToString(@"yyyy\:MM\:dd HH\:mm\:ss"));
                }
                catch
                { }
            }
        }

        /// <summary>
        /// Time when image was taken (EXIF DateTimeOriginal).
        /// </summary>
        public DateTime DateTimeOriginal
        {
            get
            {
                try
                {
                    return DateTime.ParseExact(this.GetPropertyString((int)TagNames.ExifDTOrig), @"yyyy\:MM\:dd HH\:mm\:ss", null);
                }
                catch
                {
                    return DateTime.MinValue;
                }
            }
            set
            {
                try
                {
                    this.SetPropertyString((int)TagNames.ExifDTOrig, value.ToString(@"yyyy\:MM\:dd HH\:mm\:ss"));
                }
                catch
                { }
            }
        }

        /// <summary>
        /// Time when image was digitized (EXIF DateTimeDigitized).
        /// </summary>
        public DateTime DateTimeDigitized
        {
            get
            {
                try
                {
                    return DateTime.ParseExact(this.GetPropertyString((int)TagNames.ExifDTDigitized), @"yyyy\:MM\:dd HH\:mm\:ss", null);
                }
                catch
                {
                    return DateTime.MinValue;
                }
            }
            set
            {
                try
                {
                    this.SetPropertyString((int)TagNames.ExifDTDigitized, value.ToString(@"yyyy\:MM\:dd HH\:mm\:ss"));
                }
                catch
                { }
            }
        }

        /// <summary>
        /// Image width
        /// </summary>
        public Int32 Width
        {
            get { return this._Image.Width; }
        }

        /// <summary>
        /// Image height
        /// </summary>
        public Int32 Height
        {
            get { return this._Image.Height; }
        }

        /// <summary>
        /// X resolution in dpi (EXIF XResolution/ResolutionUnit)
        /// </summary>
        public double ResolutionX
        {
            get
            {
                double R = this.GetPropertyRational((int)TagNames.XResolution).ToDouble();

                if (this.GetPropertyInt16((int)TagNames.ResolutionUnit) == 3)
                {
                    // -- resolution is in points/cm
                    return R * 2.54;
                }
                else
                {
                    // -- resolution is in points/inch
                    return R;
                }
            }
        }

        /// <summary>
        /// Y resolution in dpi (EXIF YResolution/ResolutionUnit)
        /// </summary>
        public double ResolutionY
        {
            get
            {
                double R = this.GetPropertyRational((int)TagNames.YResolution).ToDouble();

                if (this.GetPropertyInt16((int)TagNames.ResolutionUnit) == 3)
                {
                    // -- resolution is in points/cm
                    return R * 2.54;
                }
                else
                {
                    // -- resolution is in points/inch
                    return R;
                }
            }
        }

        /// <summary>
        /// Image title (EXIF ImageTitle)
        /// </summary>
        public string Title
        {
            get
            {
                return this.GetPropertyString((int)TagNames.ImageTitle);
            }
            set
            {
                try
                {
                    this.SetPropertyString((int)TagNames.ImageTitle, value);
                }
                catch { }
            }
        }

        /// <summary>
        /// User comment (EXIF UserComment)
        /// </summary>
        public string UserComment
        {
            get
            {
                return this.GetPropertyString((int)TagNames.ExifUserComment);
            }
            set
            {
                try
                {
                    this.SetPropertyString((int)TagNames.ExifUserComment, value);
                }
                catch { }
            }
        }

        /// <summary>
        /// Artist name (EXIF Artist)
        /// </summary>
        public string Artist
        {
            get
            {
                return this.GetPropertyString((int)TagNames.Artist);
            }
            set
            {
                try
                {
                    this.SetPropertyString((int)TagNames.Artist, value);
                }
                catch { }
            }
        }

        /// <summary>
        /// Image description (EXIF ImageDescription)
        /// </summary>
        public string Description
        {
            get
            {
                return this.GetPropertyString((int)TagNames.ImageDescription);
            }
            set
            {
                try
                {
                    this.SetPropertyString((int)TagNames.ImageDescription, value);
                }
                catch { }
            }
        }

        /// <summary>
        /// Image copyright (EXIF Copyright)
        /// </summary>
        public string Copyright
        {
            get
            {
                return this.GetPropertyString((int)TagNames.Copyright);
            }
            set
            {
                try
                {
                    this.SetPropertyString((int)TagNames.Copyright, value);
                }
                catch { }
            }
        }

        /// <summary>
        /// Exposure time in seconds (EXIF ExifExposureTime/ExifShutterSpeed)
        /// </summary>
        public double ExposureTimeAbs
        {
            get
            {
                if (this.IsPropertyDefined((int)TagNames.ExifExposureTime))
                    // -- Exposure time is explicitly specified
                    return this.GetPropertyRational((int)TagNames.ExifExposureTime).ToDouble();
                else
                    if (this.IsPropertyDefined((int)TagNames.ExifShutterSpeed))
                        //'-- Compute exposure time from shutter spee 
                        return (1 / Math.Pow(2, this.GetPropertyRational((int)TagNames.ExifShutterSpeed).ToDouble()));
                    else
                        // -- Can't figure out 
                        return 0;
            }
        }

        public Rational ExposureTime
        {
            get
            {
                if (this.IsPropertyDefined((int)TagNames.ExifExposureTime))
                    // -- Exposure time is explicitly specified
                    return this.GetPropertyRational((int)TagNames.ExifExposureTime);
                else
                    return new Rational();
            }
        }

        /// <summary>
        /// Aperture value as F number (EXIF ExifFNumber/ExifApertureValue)
        /// </summary>
        public double Aperture
        {
            get
            {
                if (this.IsPropertyDefined((int)TagNames.ExifFNumber))
                    return this.GetPropertyRational((int)TagNames.ExifFNumber).ToDouble();
                else
                    if (this.IsPropertyDefined((int)TagNames.ExifAperture))
                        return Math.Pow(System.Math.Sqrt(2), this.GetPropertyRational((int)TagNames.ExifAperture).ToDouble());
                    else
                        return 0;
            }
        }

        /// <summary>
        /// Exposure program used (EXIF ExifExposureProg)
        /// </summary>
        public ExposurePrograms ExposureProgram
        {
            get
            {
                Int32 X = this.GetPropertyInt16((int)TagNames.ExifExposureProg);

                if (Enum.IsDefined(typeof(ExposurePrograms), X))
                    return (ExposurePrograms)Enum.Parse(typeof(ExposurePrograms), Enum.GetName(typeof(ExposurePrograms), X));
                else
                    return ExposurePrograms.Normal;
            }
        }

        /// <summary>
        /// ISO sensitivity
        /// </summary>
        public Int16 ISO
        {
            get { return this.GetPropertyInt16((int)TagNames.ExifISOSpeed); }
        }

        /// <summary>
        /// Subject distance in meters (EXIF SubjectDistance)
        /// </summary>
        public double SubjectDistance
        {
            get { return this.GetPropertyRational((int)TagNames.ExifSubjectDist).ToDouble(); }
        }

        // 
        // 
        // 
        // 
        // 
        /// <summary>
        /// Exposure method metering mode used (EXIF MeteringMode)
        /// </summary>
        /// <remarks>
        /// If not specified, returns Unknown (0)
        /// </remarks>
        public ExposureMeteringModes ExposureMeteringMode
        {
            get
            {
                Int32 X = this.GetPropertyInt16((int)TagNames.ExifMeteringMode);

                if (Enum.IsDefined(typeof(ExposureMeteringModes), X))
                    return (ExposureMeteringModes)Enum.Parse(typeof(ExposureMeteringModes), Enum.GetName(typeof(ExposureMeteringModes), X));
                else
                    return ExposureMeteringModes.Unknown;
            }
        }

        /// <summary>
        /// Focal length of lenses in mm (EXIF FocalLength)
        /// </summary>
        public double FocalLength
        {
            get { return this.GetPropertyRational((int)TagNames.ExifFocalLength).ToDouble(); }
        }

        /// <summary>
        /// Flash mode (EXIF Flash)
        /// </summary>
        /// <remarks>
        /// If not present, value NotFired (0) is returned
        /// </remarks>
        public FlashModes FlashMode
        {
            get
            {
                Int32 X = this.GetPropertyInt16((int)TagNames.ExifFlash);

                if (Enum.IsDefined(typeof(FlashModes), X))
                    return (FlashModes)Enum.Parse(typeof(FlashModes), Enum.GetName(typeof(FlashModes), X));
                else
                    return FlashModes.NotFired;
            }
        }

        /// <summary>
        /// Light source / white balance (EXIF LightSource)
        /// </summary>
        /// <remarks>
        /// If not specified, returns Unknown (0).
        /// </remarks>
        public LightSources LightSource
        {
            get
            {
                Int32 X = this.GetPropertyInt16((int)TagNames.ExifLightSource);

                if (Enum.IsDefined(typeof(LightSources), X))
                    return (LightSources)Enum.Parse(typeof(LightSources), Enum.GetName(typeof(LightSources), X));
                else
                    return LightSources.Unknown;
            }
        }

        #endregion

        #region Support methods for working with EXIF properties

        /// <summary>
        /// Checks if current image has specified certain property
        /// </summary>
        /// <param name="PID"></param>
        /// <returns>True if image has specified property, False otherwise.</returns>
        public bool IsPropertyDefined(Int32 PID)
        {
            return (Array.IndexOf(this._Image.PropertyIdList, PID) > -1);
        }

        /// <summary>
        /// Gets specified Int32 property
        /// </summary>
        /// <param name="PID">
        /// <para>Property ID</para>
        /// <para>Optional, default 0. Default value returned if property is not present.</para>
        /// </param>
        /// <returns>Value of property or DefaultValue if property is not present.</returns>
        public Int32 GetPropertyInt32(Int32 PID)
        {
            return GetPropertyInt32(PID, 0);
        }

        public Int32 GetPropertyInt32(Int32 PID, Int32 DefaultValue)
        {
            if (IsPropertyDefined(PID))
                return GetInt32(this._Image.GetPropertyItem(PID).Value);
            else
                return DefaultValue;
        }

        /// <summary>
        /// Gets specified Int16 property
        /// </summary>
        /// <param name="PID">
        /// <para>Property ID</para>
        /// <para>Optional, default 0. Default value returned if property is not present.</para>
        /// </param>
        /// <returns>Value of property or DefaultValue if property is not present.</returns>
        public Int16 GetPropertyInt16(Int32 PID)
        {
            return GetPropertyInt16(PID, 0);
        }

        public Int16 GetPropertyInt16(Int32 PID, Int16 DefaultValue)
        {
            if (IsPropertyDefined(PID))
                return GetInt16(this._Image.GetPropertyItem(PID).Value);
            else
                return DefaultValue;
        }

        /// <summary>
        /// Gets specified string property
        /// </summary>
        /// <param name="PID">
        /// <para>Property ID</para>
        /// <para>Optional, default String.Empty. Default value returned if property is not present.</para>
        /// </param>
        /// <returns>Value of property or DefaultValue if property is not present.</returns>
        public string GetPropertyString(Int32 PID)
        {
            return GetPropertyString(PID, "");
        }

        public string GetPropertyString(Int32 PID, string DefaultValue)
        {
            if (IsPropertyDefined(PID))
                return GetString(this._Image.GetPropertyItem(PID).Value);
            else
                return DefaultValue;
        }

        /// <summary>
        /// Gets specified property in raw form
        /// </summary>
        /// <param name="PID">
        /// <para>Property ID</para>
        /// <para>Optional, default Nothing. Default value returned if property is not present.</para>
        /// </param>
        /// <param name="DefaultValue">
        /// Is recommended to use typed methods (like etc.) instead, when possible.
        /// </param>
        /// <returns></returns>
        public byte[] GetProperty(Int32 PID, byte[] DefaultValue)
        {
            if (IsPropertyDefined(PID))
                return this._Image.GetPropertyItem(PID).Value;
            else
                return DefaultValue;
        }

        public byte[] GetProperty(Int32 PID)
        {
            return GetProperty(PID, null);
        }

        /// <summary>
        /// Gets specified rational property
        /// </summary>
        /// <param name="PID">
        /// Property ID
        /// </param>
        /// <returns>Value of property or 0/1 if not present.</returns>
        public Rational GetPropertyRational(Int32 PID)
        {
            if (IsPropertyDefined(PID))
                return GetRational(this._Image.GetPropertyItem(PID).Value);
            else
            {
                Rational R;
                R.Numerator = 0;
                R.Denominator = 1;
                return R;
            }
        }

        /// <summary>
        /// Sets specified string property
        /// </summary>
        /// <param name="PID">Property ID</param>
        /// <param name="Value">Value to be set</param>
        public void SetPropertyString(Int32 PID, string Value)
        {
            byte[] Data = this._Encoding.GetBytes(Value + '\0');
            SetProperty(PID, Data, ExifDataTypes.AsciiString);
        }

        /// <summary>
        /// Sets specified Int16 property
        /// </summary>
        /// <param name="PID">Property ID</param>
        /// <param name="Value">Value to be set</param>
        public void SetPropertyInt16(Int32 PID, Int16 Value)
        {
            byte[] Data = new byte[2];
            Data[0] = (byte)(Value & 0xFF);
            Data[1] = (byte)((Value & 0xFF00) >> 8);
            SetProperty(PID, Data, ExifDataTypes.SignedShort);
        }

        /// <summary>
        /// Sets specified Int32 property
        /// </summary>
        /// <param name="PID">Property ID</param>
        /// <param name="Value">Value to be set</param>
        public void SetPropertyInt32(Int32 PID, Int32 Value)
        {
            byte[] Data = new byte[4];
            for (int I = 0; I < 4; I++)
            {
                Data[I] = (byte)(Value & 0xFF);
                Value >>= 8;
            }
            SetProperty(PID, Data, ExifDataTypes.SignedLong);
        }

        /// <summary>
        /// Sets specified property in raw form
        /// </summary>
        /// <param name="PID">Property ID</param>
        /// <param name="Data">Raw data</param>
        /// <param name="Type">EXIF data type</param>
        /// <remarks>
        /// Is recommended to use typed methods (like etc.) instead, when possible.
        /// </remarks>
        public void SetProperty(Int32 PID, byte[] Data, ExifDataTypes Type)
        {
            System.Drawing.Imaging.PropertyItem P = this._Image.PropertyItems[0];
            P.Id = PID;
            P.Value = Data;
            P.Type = (Int16)Type;
            P.Len = Data.Length;
            this._Image.SetPropertyItem(P);
        }

        /// <summary>
        /// Reads Int32 from EXIF bytearray.
        /// </summary>
        /// <param name="B">EXIF bytearray to process</param>
        /// <returns></returns>
        private Int32 GetInt32(byte[] B)
        {
            if (B.Length < 4)
                throw new ArgumentException("Data too short (4 bytes expected)", "B");

            return B[3] << 24 | B[2] << 16 | B[1] << 8 | B[0];
        }

        /// <summary>
        /// Reads Int16 from EXIF bytearray.
        /// </summary>
        /// <param name="B">EXIF bytearray to process</param>
        /// <returns></returns>
        private Int16 GetInt16(byte[] B)
        {
            if (B.Length < 2)
                throw new ArgumentException("Data too short (2 bytes expected)", "B");

            return (short)(B[1] << 8 | B[0]);
        }

        /// <summary>
        /// Reads string from EXIF bytearray.
        /// </summary>
        /// <param name="B">EXIF bytearray to process</param>
        /// <returns></returns>
        private string GetString(byte[] B)
        {
            string R = this._Encoding.GetString(B);
            if (R.EndsWith("\0"))
                R = R.Substring(0, R.Length - 1);
            return R;
        }

        /// <summary>
        /// Reads rational from EXIF bytearray.
        /// </summary>
        /// <param name="B">EXIF bytearray to process</param>
        /// <returns></returns>
        private Rational GetRational(byte[] B)
        {
            Rational R = new Rational();
            byte[] N = new byte[4];
            byte[] D = new byte[4];
            Array.Copy(B, 0, N, 0, 4);
            Array.Copy(B, 4, D, 0, 4);
            R.Denominator = this.GetInt32(D);
            R.Numerator = this.GetInt32(N);
            return R;
        }

        #endregion

        #region IDisposable implementation
        /// <summary>
        /// Disposes unmanaged resources of this class
        /// </summary>
        public void Dispose()
        {
            this._Image.Dispose();
        }

        #endregion

    }
}