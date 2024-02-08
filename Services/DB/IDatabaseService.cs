﻿using RepRepair.Models.DatabaseModels;

namespace RepRepair.Services.DB;

public interface IDatabaseService
{
    Task<ObjectInfo> GetObjectInfoByQRCodeAsync(string qrCode);
    Task<bool> InsertReportAsync(ReportInfo reportData);
}
