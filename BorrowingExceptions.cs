//Hoàng Nguyên Vũ
//202418964
using System;
namespace DeviceManagement
{
    public class BorrowingException : Exception
    {
        public BorrowingException(string message)
            : base(message)
        {
        }

        public BorrowingException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }

    public class DeviceUnavailableException : BorrowingException
    {
        private string deviceId;
        private DeviceStatus currentStatus;

        public string DeviceId
        {
            get { return deviceId; }
        }

        public DeviceStatus CurrentStatus
        {
            get { return currentStatus; }
        }

        public DeviceUnavailableException(string deviceId, DeviceStatus currentStatus)
            : base("Thiết bị '" + deviceId + "' hiện không ở trạng thái sẵn sàng (" + currentStatus + ")")
        {
            this.deviceId = deviceId;
            this.currentStatus = currentStatus;
        }
    }

    public class BorrowLimitExceededException : BorrowingException
    {
        private string borrowerId;
        private int maximumDevices;

        public string BorrowerId
        {
            get { return borrowerId; }
        }

        public int MaximumDevices
        {
            get { return maximumDevices; }
        }

        public BorrowLimitExceededException(string borrowerId, int maximumDevices)
            : base("Người mượn '" + borrowerId + "' đã vượt quá số lượng thiết bị tối đa cho phép (" + maximumDevices + " thiết bị).")
        {
            this.borrowerId = borrowerId;
            this.maximumDevices = maximumDevices;
        }
    }

    public class InvalidBorrowPeriodException : BorrowingException
    {
        private string deviceId;
        private int requestedDays;
        private int maxDays;

        public string DeviceId
        {
            get { return deviceId; }
        }

        public int RequestedDays
        {
            get { return requestedDays; }
        }

        public int MaxDays
        {
            get { return maxDays; }
        }

        public InvalidBorrowPeriodException(string deviceId, int requestedDays, int maxDays)
            : base("Thời hạn mượn yêu cầu (" + requestedDays + " ngày) cho thiết bị '" + deviceId + "' không hợp lệ hoặc vượt quá giới hạn tối đa (" + maxDays + " ngày).")
        {
            this.deviceId = deviceId;
            this.requestedDays = requestedDays;
            this.maxDays = maxDays;
        }
    }

    public class DuplicateDeviceException : BorrowingException
    {
        private string deviceId;

        public string DeviceId
        {
            get { return deviceId; }
        }

        public DuplicateDeviceException(string deviceId)
            : base("Thiết bị '" + deviceId + "' đã tồn tại trong phiếu mượn")
        {
            this.deviceId = deviceId;
        }
    }

    public class DeviceNotInSlipException : BorrowingException
    {
        private string deviceId;

        public string DeviceId
        {
            get { return deviceId; }
        }

        public DeviceNotInSlipException(string deviceId)
            : base("Thiết bị '" + deviceId + "' không thuộc phiếu mượn")
        {
            this.deviceId = deviceId;
        }
    }

    public class DeviceAlreadyReturnedException : BorrowingException
    {
        private string deviceId;

        public string DeviceId
        {
            get { return deviceId; }
        }

        public DeviceAlreadyReturnedException(string deviceId)
            : base("Thiết bị '" + deviceId + "' trong phiếu mượn này đã được trả trước đó.")
        {
            this.deviceId = deviceId;
        }
    }

    public class InvalidReturnDateException : BorrowingException
    {
        private DateTime borrowedDate;
        private DateTime returnedDate;

        public DateTime BorrowedDate
        {
            get { return borrowedDate; }
        }

        public DateTime ReturnedDate
        {
            get { return returnedDate; }
        }

        public InvalidReturnDateException(DateTime borrowedDate, DateTime returnedDate)
            : base("Ngày trả không được trước ngày mượn")
        {
            this.borrowedDate = borrowedDate;
            this.returnedDate = returnedDate;
        }
    }
}