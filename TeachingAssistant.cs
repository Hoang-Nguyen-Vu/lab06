//Hoàng Nguyên Vũ
//202419020
using System;
namespace DeviceManagement
{
    public class TeachingAssistant : IBorrower
    {
        private string borrowerId;
        private string fullName;
        private string assignedCourse;

        public string BorrowerId
        {
            get { return borrowerId; }
        }

        public string FullName
        {
            get { return fullName; }
        }

        public string AssignedCourse
        {
            get { return assignedCourse; }
            set { assignedCourse = value; }
        }

        public TeachingAssistant(string borrowerId, string fullName, string assignedCourse = "")
        {
            if (borrowerId == null || borrowerId == "")
            {
                throw new ArgumentException("Mã trợ giảng không được để trống.");
            }
            if (fullName == null || fullName == "")
            {
                throw new ArgumentNullException("fullName");
            }

            this.borrowerId = borrowerId;
            this.fullName = fullName;
            this.assignedCourse = assignedCourse;
        }

        public int GetMaximumDevices()
        {
            return 2;
        }

        public override string ToString()
        {
            return "Trợ giảng: [" + borrowerId + "] " + fullName + " - Môn: " + assignedCourse;
        }
    }
}