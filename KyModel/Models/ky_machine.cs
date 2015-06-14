using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SqlFu;
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
        [QueryOnly]
        public bool startBusinessCtl { get; set; }
        /// <summary>
        /// �ļ���
        /// </summary>
        [QueryOnly]
        public string fileName { get; set; }
        /// <summary>
        /// ���׿��ƿ�ʼʱ�䣬��Ҫ�������ֽ��׿���֮ǰ�µĳ�
        /// </summary>
        [QueryOnly]
        public DateTime dateTime { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        [QueryOnly]
        public BussinessType business { get; set; }
        /// <summary>
        /// ÿ����Ʊ����
        /// </summary>
        [QueryOnly]
        public int bundleCount { get; set; }
        /// <summary>
        /// �û�ID
        /// </summary>
        [QueryOnly]
        public int userId { get; set; }
        /// <summary>
        /// ͼ�����ݿ�ID
        /// </summary>
        [QueryOnly]
        public int imgServerId { get; set; }
        /// <summary>
        /// �����ļ�����ID
        /// </summary>
        [QueryOnly]
        public int importMachineId { get; set; }
        /// <summary>
        /// ҵ����ˮ��
        /// </summary>
        [QueryOnly]
        public string bussinessNumber { get; set; }
        /// <summary>
        /// ATM���
        /// </summary>
        [QueryOnly]
        public int atmId { get; set; }
        /// <summary>
        /// ������
        /// </summary>
        [QueryOnly]
        public int cashBoxId { get; set; }
        /// <summary>
        /// �Ƿ��������
        /// </summary>
        [QueryOnly]
        public string isClearCenter { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        [QueryOnly]
        public string packageNumber { get; set; }
        /// <summary>
        /// ����ʱ��
        /// </summary>
        [QueryOnly]
        public DateTime alive { get; set; }
        /// <summary>
        /// ҵ����ʱĿ¼
        /// </summary>
        [QueryOnly]
        public string tmpPath { get; set; }
    }
}
