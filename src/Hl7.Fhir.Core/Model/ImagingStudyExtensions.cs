using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Hl7.Fhir.Model
{
    public static class ImagingStudyExtensions
    {
        /// <summary>
        /// Given a collection of ImagingStudy resources, return a flat list of all Instances present in those ImagingStudy resources
        /// </summary>
        /// <param name="studies">A collection of ImagingStudy resources</param>
        /// <returns>A single flat list of all Instances in the studies</returns>
        public static IEnumerable<ImagingStudy.ImagingStudySeriesInstanceComponent> ListInstances(this IEnumerable<ImagingStudy> studies)
        {
            if (studies == null) throw new ArgumentNullException("studies");

            return ListSeries(studies).SelectMany(serie => serie.Instance != null ? serie.Instance : new List<ImagingStudy.ImagingStudySeriesInstanceComponent>());
        }


        /// <summary>
        /// Given a collection of ImagingStudy resources, return a flat list of all Series present in those ImagingStudy resources
        /// </summary>
        /// <param name="studies">A collection of ImagingStudy resources</param>
        /// <returns>A single flat list of all Series in the studies</returns>
        public static IEnumerable<ImagingStudy.ImagingStudySeriesComponent> ListSeries(this IEnumerable<ImagingStudy> studies)
        {
            if (studies == null) throw new ArgumentException("studies");

            return studies.SelectMany(study => study.Series != null ? study.Series : new List<ImagingStudy.ImagingStudySeriesComponent>());
        }
    }
}
