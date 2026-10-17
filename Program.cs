//Hoàng Nguyên Vũ
//202419020
using System;
using DeviceManagement;
class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        DateTime borrowDate = new DateTime(2026, 10, 10, 8, 0, 0);

        Console.WriteLine("===== 11.1. LUỒNG THÀNH CÔNG =====");
        Lecturer gv = new Lecturer("GV01", "Nguyễn Văn A", "CNTT");
        ProjectorRemote pj = new ProjectorRemote("PJ01", "Remote máy chiếu Epson", 2024, 500000m, 1);
        AirConditionerRemote ac = new AirConditionerRemote("AC01", "Remote điều hòa Daikin", 2023, 300000m, 1);
        Microphone mic = new Microphone("MIC01", "Micro Shure", 2022, 2000000m, 1, true);

        ClassRoom room = new ClassRoom("P101", "Phòng 101", 10);
        room.AddDevice(pj); room.AddDevice(ac); room.AddDevice(mic);
        Console.WriteLine(room);
        Console.WriteLine("Chi phí bảo trì năm của phòng: " + room.CalculateAnnualMaintenanceCost().ToString("N0") + " VNĐ");

        BorrowingSlip slip1 = new BorrowingSlip("S01", gv, "P101", borrowDate);
        try
        {
            slip1.AddDevice(pj, 1);
            slip1.AddDevice(mic, 1);
            decimal f1 = slip1.ReturnDevice("PJ01", borrowDate.AddDays(1));
            Console.WriteLine("Trả PJ01 đúng hạn, phí trễ = " + f1.ToString("N0"));
            decimal f2 = slip1.ReturnDevice("MIC01", borrowDate.AddDays(3));
            Console.WriteLine("Trả MIC01 muộn 2 ngày, phí trễ = " + f2.ToString("N0"));
        }
        catch (BorrowingException ex) { Console.WriteLine("Lỗi: " + ex.Message); }

        BorrowingSlip slip2 = new BorrowingSlip("S02", gv, "P102", borrowDate);
        slip2.AddDevice(ac, 1);

        BorrowingSlip[] slips = { slip1, slip2 };
        Console.WriteLine("\n--- Các mục trả muộn ---");
        foreach (BorrowingSlip s in slips)
            foreach (BorrowItem it in s.Items)
                if (it.IsReturned && it.ReturnedDate.Value.Date > it.DueDate.Date)
                    Console.WriteLine(s.SlipId + " " + it);
        Console.WriteLine("--- Các mục chưa trả ---");
        foreach (BorrowingSlip s in slips)
            foreach (BorrowItem it in s.Items)
                if (!it.IsReturned)
                    Console.WriteLine(s.SlipId + " " + it);
        Console.WriteLine("Tổng phí trễ phiếu S01: " + slip1.CalculateTotalLateFee().ToString("N0"));

        Console.WriteLine("\n===== 11.2. LUỒNG NGOẠI LỆ =====");
        Lecturer gv2 = new Lecturer("GV02", "Trần Thị B", "Toán");
        TeachingAssistant ta = new TeachingAssistant("TA01", "Lê Văn C", "OOP");

        Run("1. Mượn remote điều hòa đang UnderMaintenance", () =>
        {
            var acm = new AirConditionerRemote("AC02", "Remote điều hòa hỏng", 2020, 300000m, 1, DeviceStatus.UnderMaintenance);
            new BorrowingSlip("E1", gv2, "P201", borrowDate).AddDevice(acm, 1);
        });

        Run("2. Trợ giảng mượn thiết bị thứ ba", () =>
        {
            var s = new BorrowingSlip("E2", ta, "P202", borrowDate);
            s.AddDevice(new ProjectorRemote("PJ10", "Remote 10", 2024, 400000m, 1), 1);
            s.AddDevice(new ProjectorRemote("PJ11", "Remote 11", 2024, 400000m, 1), 1);
            s.AddDevice(new ProjectorRemote("PJ12", "Remote 12", 2024, 400000m, 1), 1);
        });

        Run("3. Thêm cùng một micro hai lần", () =>
        {
            var m = new Microphone("MIC10", "Micro 10", 2024, 1000000m, 1, false);
            var s = new BorrowingSlip("E3", gv2, "P203", borrowDate);
            s.AddDevice(m, 1);
            s.AddDevice(m, 1);
        });

        Run("4. Mượn micro 3 ngày (giới hạn 1 ngày)", () =>
        {
            var m = new Microphone("MIC11", "Micro 11", 2024, 1000000m, 1, false);
            var s = new BorrowingSlip("E4", gv2, "P204", borrowDate);
            try { s.AddDevice(m, 3); }
            finally { Console.WriteLine("   Sau lỗi: micro = " + m.Status + ", số item trong phiếu = " + s.Items.Length); }
        });

        Run("5. Trả remote máy chiếu không thuộc phiếu", () =>
        {
            var s = new BorrowingSlip("E5", gv2, "P205", borrowDate);
            s.AddDevice(new Microphone("MIC12", "Micro 12", 2024, 1000000m, 1, false), 1);
            s.ReturnDevice("PJ99", borrowDate.AddDays(1));
        });

        Run("6. Trả cùng một thiết bị hai lần", () =>
        {
            var s = new BorrowingSlip("E6", gv2, "P206", borrowDate);
            s.AddDevice(new ProjectorRemote("PJ20", "Remote 20", 2024, 400000m, 1), 1);
            s.ReturnDevice("PJ20", borrowDate.AddDays(1));
            s.ReturnDevice("PJ20", borrowDate.AddDays(1));
        });

        Run("7. Trả với ngày trả trước ngày mượn", () =>
        {
            var p = new ProjectorRemote("PJ30", "Remote 30", 2024, 400000m, 1);
            var s = new BorrowingSlip("E7", gv2, "P207", borrowDate);
            s.AddDevice(p, 1);
            try { s.ReturnDevice("PJ30", borrowDate.AddDays(-1)); }
            finally { Console.WriteLine("   Trạng thái thiết bị sau lỗi: " + p.Status + " (vẫn Borrowed)"); }
        });
    }

    static void Run(string title, Action action)
    {
        Console.WriteLine("\n" + title);
        try
        {
            action();
            Console.WriteLine("   (không có lỗi)");
        }
        catch (BorrowingException ex)
        {
            Console.WriteLine("   Bắt " + ex.GetType().Name + ": " + ex.Message);
        }
        finally
        {
            Console.WriteLine("   [finally] kết thúc tình huống.");
        }
    }
}