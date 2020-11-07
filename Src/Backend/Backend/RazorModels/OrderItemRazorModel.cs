﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.RazorModels
{
    using AutoMapper;
    using DataAccessLayer.Models;
    using Backend.AdapterModels;
    using Backend.Interfaces;
    using Backend.Services;
    using Backend.Helpers;
    using Backend.SortModels;
    using Syncfusion.Blazor.Grids;
    using Microsoft.AspNetCore.Components.Forms;
    using ShareDomain.DataModels;

    public class OrderItemRazorModel
    {
        #region Constructor
        public OrderItemRazorModel(IOrderItemService CurrentService,
           SchoolContext context,
           IMapper Mapper)
        {
            this.CurrentService = CurrentService;
            this.context = context;
            mapper = Mapper;
            OrderItemSort.Initialization(SortConditions);
        }
        #endregion

        #region Property
        public bool IsShowEditRecord { get; set; } = false;
        public OrderItemAdapterModel CurrentRecord { get; set; } = new OrderItemAdapterModel();
        public OrderItemAdapterModel CurrentNeedDeleteRecord { get; set; } = new OrderItemAdapterModel();
        public EditContext LocalEditContext { get; set; }
        public bool ShowAontherRecordPicker { get; set; } = false;
        public MasterRecord Header { get; set; } = new MasterRecord();
        public string HeaderTitle
        {
            get
            {
                if (Header == null || string.IsNullOrEmpty(Header.Title))
                {
                    return "";
                }
                else
                {
                    return $"{Header.Title} 的 訂單清單";
                }
            }
        }
        public List<SortCondition> SortConditions { get; set; } = new List<SortCondition>();
        public SortCondition CurrentSortCondition { get; set; } = new SortCondition();

        #region 訊息說明之對話窗使用的變數
        public ConfirmBoxModel ConfirmMessageBox { get; set; } = new ConfirmBoxModel();
        public MessageBoxModel MessageBox { get; set; } = new MessageBoxModel();
        #endregion

        public string EditRecordDialogTitle { get; set; } = "";
        #endregion

        #region Field
        bool isNewRecordMode;
        private readonly IOrderItemService CurrentService;
        private readonly SchoolContext context;
        private readonly IMapper mapper;
        IRazorPage thisRazorComponent;
        IDataGrid dataGrid;
        private bool isShowConfirm { get; set; } = false;
        #endregion

        #region Method
        #region DataGrid 初始化
        public void Setup(IRazorPage razorPage, IDataGrid dataGrid)
        {
            thisRazorComponent = razorPage;
            this.dataGrid = dataGrid;
        }
        #endregion

        #region 工具列事件 (新增)
        public void ToolbarClickHandler(Syncfusion.Blazor.Navigations.ClickEventArgs args)
        {
            if (args.Item.Text == "新增")
            {
                CurrentRecord = new OrderItemAdapterModel();
                EditRecordDialogTitle = "新增紀錄";
                isNewRecordMode = true;
                IsShowEditRecord = true;
                CurrentRecord.OrderId = Header.Id;
                CurrentRecord.OrderName = Header.Title;
            }
            else if (args.Item.Text == "重新整理")
            {
                dataGrid.RefreshGrid();
            }
        }
        #endregion

        #region 記錄列的按鈕事件 (修改與刪除)
        public void OnCommandClicked(CommandClickEventArgs<OrderItemAdapterModel> args)
        {
            OrderItemAdapterModel item = args.RowData as OrderItemAdapterModel;
            if (args.CommandColumn.ButtonOption.Content == "修改")
            {
                CurrentRecord = item;
                EditRecordDialogTitle = "修改紀錄";
                IsShowEditRecord = true;
                isNewRecordMode = false;

            }
            else if (args.CommandColumn.ButtonOption.Content == "刪除")
            {
                #region 檢查關聯資料是否存在
                #endregion
                CurrentNeedDeleteRecord = item;
                ConfirmMessageBox.Show("400px", "200px", "警告", "確認要刪除這筆紀錄嗎？");
            }
        }

        public async Task RemoveThisRecord(bool NeedDelete)
        {
            if (NeedDelete == true)
            {
                await CurrentService.DeleteAsync(mapper.Map<OrderItem>(CurrentNeedDeleteRecord));
                dataGrid.RefreshGrid();
            }
            ConfirmMessageBox.Hidden();
        }
        #endregion

        #region 修改紀錄對話窗的按鈕事件
        public void OnEditContestChanged(EditContext context)
        {
            LocalEditContext = context;
        }

        public void OnRecordEditCancel()
        {
            IsShowEditRecord = false;
        }

        public async Task OnRecordEditConfirm()
        {
            #region 進行 Form Validation 檢查驗證作業
            if (LocalEditContext.Validate() == false)
            {
                return;
            }
            #endregion

            #region 檢查資料完整性
            if (isNewRecordMode == true)
            {
                var checkedResult = await CurrentService
                    .BeforeAddCheckAsync(mapper.Map<OrderItem>(CurrentRecord));
                if (checkedResult == false)
                {
                    ConfirmMessageBox.Show("400px", "200px", "警告", "該學生已經存在該課程內，無法完成新增");
                    return;
                }
            }
            else
            {
                var checkedResult = await CurrentService
                    .BeforeUpdateCheckAsync(mapper.Map<OrderItem>(CurrentRecord));
                if (checkedResult == false)
                {
                    ConfirmMessageBox.Show("400px", "200px", "警告", "該學生已經存在該課程內，無法完成修改");
                    return;
                }
            }
            #endregion

            if (IsShowEditRecord == true)
            {
                if (isNewRecordMode == true)
                {
                    await CurrentService.AddAsync(mapper.Map<OrderItem>(CurrentRecord));
                    dataGrid.RefreshGrid();
                }
                else
                {
                    await CurrentService.UpdateAsync(mapper.Map<OrderItem>(CurrentRecord));
                    dataGrid.RefreshGrid();
                }
                IsShowEditRecord = false;
            }
        }
        #endregion

        #region 開窗選取紀錄使用到的方法
        public void OnOpenPicker()
        {
            ShowAontherRecordPicker = true;
        }

        public void OnPickerCompletion(ProductAdapterModel e)
        {
            if (e != null)
            {
                CurrentRecord.ProductId = e.ProductId;
                CurrentRecord.ProductName = e.Name;
            }
            ShowAontherRecordPicker = false;
        }
        #endregion

        #region 資料表關聯的方法
        public async Task UpdateMasterHeaderAsync(MasterRecord header)
        {
            Header = header;
            await Task.Delay(100);
            dataGrid.RefreshGrid();
        }
        #endregion

        #region 排序搜尋事件

        public void SortChanged(Syncfusion.Blazor.DropDowns.ChangeEventArgs<int, SortCondition> args)
        {
            if (dataGrid.GridIsExist() == true)
            {
                CurrentSortCondition.Id = args.Value;
                dataGrid.RefreshGrid();
            }
        }

        #endregion
        #endregion
    }
}
