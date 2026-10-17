//Hoàng Nguyên Vũ
//202419020
using System;
namespace DeviceManagement
{
    public class Lecturer : IBorrower
    {
        private string borrowerId;
        private string fullName;
        private string department;

        public string BorrowerId
        {
            get { return borrowerId; }
        }

        public string FullName
        {
            get { return fullName; }
        }

        public string Department
        {
            get { return department; }
            set { department = value; }
        }

        public Lecturer(string borrowerId, string fullName, string department = "")
        {
            if (borrowerId == null || borrowerId == "")
            {
                throw new ArgumentException("Mã giảng viên không được để trống.");
            }
            if (fullName == null || fullName == "")
            {
                throw new ArgumentNullException("fullName");
            }

            this.borrowerId = borrowerId;
            this.fullName = fullName;
            this.department = department;
        }

        public int GetMaximumDevices()
        {
            return 3;
        }

        public override string ToString()
        {
            return "Giảng viên: " + borrowerId + " " + fullName + " - Khoa: " + department;
        }
    }
}