//Hoàng Nguyên Vũ 
//202419020
namespace DeviceManagement
{
    public interface IBorrower
    {
        string BorrowerId { get; }
        string FullName { get; }
        int GetMaximumDevices();
    }
}