using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.IO;
using Hl7.Fhir.Support;
using System.Text.RegularExpressions;
using System.Xml;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Api.Serialization;

namespace Hl7.Fhir.Test
{
    [TestClass]
    public class DicomTests
    {
        [TestMethod]
        public void SerializeImagingStudyAllAttr()
        {
            var study = createImagingStudy();
            var actual = DicomSerializer.SerializeStudyToXml(study);

            
        }

        private ImagingStudy createImagingStudy()
        {
            var res = new ImagingStudy();

            res.DateTime = "2014-05-16T15:21:45-07:00";

            return res;
        }
    }
}
