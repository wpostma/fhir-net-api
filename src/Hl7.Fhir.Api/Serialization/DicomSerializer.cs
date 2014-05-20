using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Hl7.Fhir.Api.Serialization
{
    //ImagingStudy	Reference IHE radiology TF vol 2 table 4.14.1
    //dateTime	0008,0020+0008,0030
    //subject	0010,*
    //uid	0020,000D
    //accessionNo	0008,0050
    //identifier	0020,0010
    //order	
    //modality	0008,0061
    //referrer	0008,0090
    //availability	0008,0056
    //url	0008,1190
    //numberOfSeries	(0020,1206)
    //numberOfInstances	(0020,1208)
    //clinicalInformation	0040,1002, 0008,1080
    //procedure	0008,1032
    //interpreter	0008,1060
    //description	0008,1030
    //series	
    //    number	0020,0011
    //    modality	0008,0060
    //    uid	0020,000E
    //    description	0008,103E
    //    numberOfInstances	0020,1209
    //    availability	0008,0056
    //    url	0008,1115 > 0008,1190
    //    bodySite	0018,0015
    //    dateTime	
    //    instance	
    //        number	0020,0013
    //        uid	0008,0018
    //        sopclass	0008,0016
    //        type	0004,1430
    //        title	0070,0080 | 0040,A043 > 0008,0104 | 0042,0010 | 0008,0008
    //        url	0008,1199 > 0008,1190
    //        attachment	



    public enum ValueRepresentation
    {
        AE,	//Application Entity
        AS,	//Age String
        AT,	//Attribute Tag
        CS,	//Code String
        DA,	//Date
        DS,	//Decimal String
        DT,	//Date Time
        FL,	//Floating Point Single
        FD,	//Floating Point Double
        IS,	//Integer String
        LO,	//Long String
        LT,	//Long Text
        OB,	//Other Byte String
        OF,	//Other Float String
        OW,	//Other Word String
        PN,	//Person Name
        SH,	//Short String
        SL,	//Signed Long
        SQ,	//Sequence of Items
        SS,	//Signed Short
        ST,	//Short Text
        TM,	//Time
        UI,	//Unique Identifier
        UL,	//Unsigned Long
        UN,	//Unknown
        US,	//Unsigned Short
        UT,	//Unlimited Text
    }

    public class DicomElement
    {
        public DicomElement(string tag, string key, ValueRepresentation vr)
        {
            Tag = tag;
            Keyword = key;
            Vr = vr;
        }

        public string Tag { get; internal set; }
        public string Keyword { get; internal set; }
        public ValueRepresentation Vr { get; internal set; }
    }



    public class DicomSerializer
    {
        public const string STUDYDATE_TAG = "00080020";
        public const string STUDYTIME_TAG = "00080030";

        public static List<DicomElement> DicomDictionary = new List<DicomElement> { 
            new DicomElement(STUDYDATE_TAG, "StudyDate", ValueRepresentation.DA), 
            new DicomElement(STUDYTIME_TAG, "StudyTime", ValueRepresentation.TM) 
        };

        public static XDocument SerializeStudyToXml(ImagingStudy study, IEnumerable<string> attributes = null)
        {
            var result = createDicomObject();

            if (study.DateTimeElement != null && emitAttribute(STUDYDATE_TAG, attributes) )
                addValue(createAttribute(result, STUDYDATE_TAG), ConvertToDA(study.DateTimeElement));

            if(study.DateTimeElement != null && emitAttribute(STUDYTIME_TAG, attributes))
                addValue(createAttribute(result, STUDYTIME_TAG), ConvertToTM(study.DateTimeElement));

            return result;
        }


        private static bool emitAttribute(string tag, IEnumerable<string> attributes)
        {
            if (attributes == null) return true;

            return attributes.Contains(STUDYDATE_TAG);
        }

        private static XDocument createDicomObject()
        {
            return new XDocument(new XElement("NativeDicomModel"));            
        }


        private static XElement addValue(XElement parent, string atomic)
        {
            int number = parent.Elements("Value").Count();

            var result = new XElement("Value", new XAttribute("number", number + 1), new XText(atomic));

            parent.Add(result);

            return result;
        }

        private static XElement createAttribute(XDocument document, string tag)
        {
            var element = DicomDictionary.SingleOrDefault(elem => elem.Tag == tag);

            if (element == null) throw new ArgumentException("Unsupported DICOM tag " + tag + " found", "tag");

            var newNode = new XElement("DicomAttribute",
                        new XAttribute("Tag", tag),
                        new XAttribute("VR", element.Vr.ToString()),
                        new XAttribute("Keyword", element.Keyword ));

            document.Root.Add(newNode);

            return newNode;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        /// <remarks>If the timezone is not specified, the local timezone is assumed</remarks>
        public static string ConvertToDA(FhirDateTime dt, TimeSpan? zone = null)
        {
            var dto = dt.ToDateTimeOffset(zone);

            return dto.ToString("yyyyMMdd");
        }

        public static string ConvertToTM(FhirDateTime dt, TimeSpan? zone = null)
        {
            var dto = dt.ToDateTimeOffset(zone);

            return dto.ToString("HHmmss");
        }
    }
}
