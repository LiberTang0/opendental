using System;
using OpenDentBusiness;
using System.Data;

namespace OpenDentalGraph.Cache {
	public class DashboardCacheNewPatient:DashboardCacheWithQuery<NewPatient> {
		protected override string GetCommand(DashboardFilter filter) {
			//todo: this should use patient.DateFirstVisit instead. Much faster but this would require some development and a convert script.
			return
				"SELECT PatNum, MIN(ProcDate) FirstProc "
				+"FROM procedurelog USE INDEX(indexPNPSCN) "
				+"INNER JOIN procedurecode ON procedurecode.CodeNum = procedurelog.CodeNum "
												+"AND procedurecode.ProcCode NOT IN ('D9986','D9987')"
				+"WHERE ProcStatus="+POut.Int((int)ProcStat.C)+" "
				+"GROUP BY PatNum";
		}

		protected override NewPatient GetInstanceFromDataRow(DataRow x) {
			return new NewPatient() {
				DateStamp=x.Field<DateTime>("FirstProc"),
				Count=1, //Each row counts as 1.
				Val=0, //there are no fees
				SeriesName="All", //Only 1 series.
			};
		}

		protected override bool AllowQueryDateFilter() {
			return false;
		}
	}

	public class NewPatient:GraphQuantityOverTime.GraphPointBase { }
}