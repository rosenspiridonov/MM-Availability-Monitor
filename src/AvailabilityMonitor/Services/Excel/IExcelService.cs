namespace AvailabilityMonitor.Services.Excel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IExcelService
    {
        void GenerateFile<T>(List<T> obj);
    }
}
