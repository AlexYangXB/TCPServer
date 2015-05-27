using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace KyModel.Models
{
    public partial class ky_machine
    {
        public int kId { get; set; }
        public int kMachineType { get; set; }
        public string kMachineNumber { get; set; }
        public string kMachineModel { get; set; }
        public string kIpAddress { get; set; }
        public int kStatus { get; set; }
        public int kNodeId { get; set; }
        public int kFactoryId { get; set; }
        public Nullable<System.DateTime> kUpdateTime { get; set; }
        //����� ���ڽ��׿���
        /// <summary>
        /// ��ʼ���׿���
        /// </summary>
        public bool startBusinessCtl { get; set; }
        /// <summary>
        /// �ļ���
        /// </summary>
        public string fileName { get; set; }
        /// <summary>
        /// ���׿��ƿ�ʼʱ�䣬��Ҫ�������ֽ��׿���֮ǰ�µĳ�
        /// </summary>
        public DateTime dateTime { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        public BussinessType business { get; set; }
        /// <summary>
        /// ÿ����Ʊ����
        /// </summary>
        public int bundleCount { get; set; }
        /// <summary>
        /// �û�ID
        /// </summary>
        public int userId { get; set; }
        /// <summary>
        /// ͼ�����ݿ�ID
        /// </summary>
        public int imgServerId { get; set; }
        /// <summary>
        /// �����ļ�����ID
        /// </summary>
        public int importMachineId { get; set; }
        /// <summary>
        /// ҵ����ˮ��
        /// </summary>
        public string bussinessNumber { get; set; }
        /// <summary>
        /// ATM���
        /// </summary>
        public int atmId { get; set; }
        /// <summary>
        /// ������
        /// </summary>
        public int cashBoxId { get; set; }
        /// <summary>
        /// �Ƿ��������
        /// </summary>
        public string isClearCenter { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        public string packageNumber { get; set; }
    }
}
