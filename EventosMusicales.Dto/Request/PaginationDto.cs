using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace EventosMusicales.Dto.Request
{
    public class PaginationDto
    {
		private readonly int maxRecordsPerPage = 20;
		public int Page { get; set; } = 1;

		private int recordPerPage = 10;

		public int RecordPerPage
		{
			get { return recordPerPage; }
			set { recordPerPage = value > maxRecordsPerPage ? maxRecordsPerPage : value; }
		}

	}
}
