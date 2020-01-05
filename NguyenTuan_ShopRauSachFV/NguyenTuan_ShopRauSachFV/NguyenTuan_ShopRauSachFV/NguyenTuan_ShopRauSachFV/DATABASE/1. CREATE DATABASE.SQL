--create database [RAU_SACH_FV]
--go
USE [RAU_SACH_FV]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[cau_hinh](
	[ma_cau_hinh] [varchar](50) NOT NULL,
	[noi_dung] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_cau_hinh] PRIMARY KEY CLUSTERED 
(
	[ma_cau_hinh] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[danh_muc](
	[id_dm] [int] IDENTITY(1,1) NOT NULL,
	[id_dm_cha] [int] NULL,
	[ten_dm] [nvarchar](50) NOT NULL,
	[show_in_menu] [bit] NOT NULL CONSTRAINT [DF_danh_muc_show_in_menu] DEFAULT ((0)),
	[stt] [int] NOT NULL CONSTRAINT [DF_danh_muc_stt] DEFAULT ((0)),
 CONSTRAINT [PK_danh_muc] PRIMARY KEY CLUSTERED 
(
	[id_dm] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [Unique__ten_dm__danh_muc] UNIQUE NONCLUSTERED 
(
	[ten_dm] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[don_hang](
	[id_dh] [int] IDENTITY(1,1) NOT NULL,
	[id_tv] [int] NOT NULL,
	[ngay_dh] [tinyint] NOT NULL,
	[thang_dh] [tinyint] NOT NULL,
	[nam_dh] [smallint] NOT NULL,
	[thanh_tien] [bigint] NULL,
	[khoa] [bit] NULL,
	[ly_do_khoa] [nvarchar](500) NULL,
 CONSTRAINT [PK_don_hang] PRIMARY KEY CLUSTERED 
(
	[id_dh] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[don_hang_chi_tiet](
	[id_dhct] [int] IDENTITY(1,1) NOT NULL,
	[id_dh] [int] NOT NULL,
	[id_sp] [int] NOT NULL,
	[don_gia] [bigint] NOT NULL,
	[so_luong] [int] NOT NULL,
	[thanh_tien]  AS ([so_luong]*[don_gia]),
	[id_dvt] [int] NOT NULL,
 CONSTRAINT [PK_don_hang_chi_tiet] PRIMARY KEY CLUSTERED 
(
	[id_dhct] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [Unique__id_dh__id_sp__don_hang_chi_tiet] UNIQUE NONCLUSTERED 
(
	[id_dh] ASC,
	[id_sp] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[don_vi_tinh](
	[id_dvt] [int] IDENTITY(1,1) NOT NULL,
	[ten_dvt] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_don_vi_tinh] PRIMARY KEY CLUSTERED 
(
	[id_dvt] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [unique__ten_dvt__don_vi_tinh] UNIQUE NONCLUSTERED 
(
	[ten_dvt] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[gio_hang](
	[id_tv] [int] NOT NULL,
	[id_sp] [int] NOT NULL,
	[don_gia] [bigint] NOT NULL,
	[so_luong] [int] NOT NULL,
	[thanh_tien]  AS ([so_luong]*[don_gia]),
	[id_dvt] [int] NOT NULL,
 CONSTRAINT [Unique__id_tv__id_sp__gio_hang] UNIQUE NONCLUSTERED 
(
	[id_tv] ASC,
	[id_sp] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[hoi_dap](
	[id_hd] [int] IDENTITY(1,1) NOT NULL,
	[id_tv] [int] NOT NULL,
	[tieu_de] [nvarchar](50) NOT NULL,
	[noi_dung] [nvarchar](max) NOT NULL,
	[ngay_hd] [tinyint] NOT NULL,
	[thang_hd] [tinyint] NOT NULL,
	[nam_hd] [smallint] NOT NULL,
	[close_hd] [bit] NULL,
	[chia_se] [bit] NULL,
 CONSTRAINT [PK_hoi_dap] PRIMARY KEY CLUSTERED 
(
	[id_hd] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[hoi_dap_tra_loi](
	[id_traloi] [int] IDENTITY(1,1) NOT NULL,
	[id_hd] [int] NOT NULL,
	[id_tv] [int] NOT NULL,
	[noi_dung] [nvarchar](max) NOT NULL,
	[ngay_traloi] [tinyint] NOT NULL,
	[thang_traloi] [tinyint] NOT NULL,
	[nam_traloi] [smallint] NOT NULL,
 CONSTRAINT [PK_hoi_dap_tra_loi] PRIMARY KEY CLUSTERED 
(
	[id_traloi] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[quyen_han](
	[ma_quyen] [varchar](50) NOT NULL,
	[ten_quyen] [nvarchar](50) NOT NULL,
	[khoa] [bit] NULL,
 CONSTRAINT [PK_quyen_han] PRIMARY KEY CLUSTERED 
(
	[ma_quyen] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[san_pham](
	[id_sp] [int] IDENTITY(1,1) NOT NULL,
	[id_dm] [int] NOT NULL,
	[ten_sp] [nvarchar](50) NOT NULL,
	[gioi_thieu] [nvarchar](max) NOT NULL,
	[ngay_sp] [tinyint] NOT NULL,
	[thang_sp] [tinyint] NOT NULL,
	[nam_sp] [smallint] NOT NULL,
	[don_gia] [bigint] NOT NULL,
	[luot_xem] [bigint] NOT NULL,
	[id_dvt] [int] NOT NULL,
 CONSTRAINT [PK_san_pham] PRIMARY KEY CLUSTERED 
(
	[id_sp] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[thanh_vien](
	[id_tv] [int] IDENTITY(1,1) NOT NULL,
	[ma_quyen] [varchar](50) NOT NULL,
	[account_name] [varchar](50) NULL,
	[email] [varchar](50) NOT NULL,
	[mat_khau] [varchar](32) NOT NULL,
	[ho_ten] [nvarchar](50) NOT NULL,
	[gioi_tinh] [bit] NOT NULL,
	[ngay_sinh] [tinyint] NOT NULL,
	[thang_sinh] [tinyint] NOT NULL,
	[nam_sinh] [smallint] NOT NULL,
	[dia_chi] [nvarchar](max) NOT NULL,
	[sdt] [bigint] NOT NULL,
	[khoa] [bit] NULL,
	[ly_do_khoa] [nvarchar](500) NULL,
 CONSTRAINT [PK_khach_hang] PRIMARY KEY CLUSTERED 
(
	[id_tv] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [Unique__account_name__thanh_vien] UNIQUE NONCLUSTERED 
(
	[account_name] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [Unique__email__thanh_vien] UNIQUE NONCLUSTERED 
(
	[email] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [Unique__sdt__thanh_vien] UNIQUE NONCLUSTERED 
(
	[sdt] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tin_tuc](
	[id_tt] [int] IDENTITY(1,1) NOT NULL,
	[id_tv] [int] NOT NULL,
	[tieu_de] [nvarchar](50) NOT NULL,
	[noi_dung] [nvarchar](max) NOT NULL,
	[ngay_tt] [tinyint] NOT NULL,
	[thang_tt] [tinyint] NOT NULL,
	[nam_tt] [smallint] NOT NULL,
	[luot_xem] [bigint] NOT NULL,
	[ky_thuat] [bit] NULL,
	[luu_nhap] [bit] NULL,
 CONSTRAINT [PK_tin_tuc] PRIMARY KEY CLUSTERED 
(
	[id_tt] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[don_hang]  WITH CHECK ADD  CONSTRAINT [FK_don_hang_thanh_vien] FOREIGN KEY([id_tv])
REFERENCES [dbo].[thanh_vien] ([id_tv])
GO

ALTER TABLE [dbo].[don_hang] CHECK CONSTRAINT [FK_don_hang_thanh_vien]
GO

ALTER TABLE [dbo].[don_hang]  WITH CHECK ADD  CONSTRAINT [ck__nam_dh__don_hang] CHECK  (([nam_dh]>(2013)))
GO

ALTER TABLE [dbo].[don_hang] CHECK CONSTRAINT [ck__nam_dh__don_hang]
GO

ALTER TABLE [dbo].[don_hang]  WITH CHECK ADD  CONSTRAINT [ck__ngay_dh__don_hang] CHECK  (([ngay_dh]>(0) AND [ngay_dh]<(32)))
GO

ALTER TABLE [dbo].[don_hang] CHECK CONSTRAINT [ck__ngay_dh__don_hang]
GO

ALTER TABLE [dbo].[don_hang]  WITH CHECK ADD  CONSTRAINT [ck__thang_dh__don_hang] CHECK  (([thang_dh]>(0) AND [thang_dh]<(13)))
GO

ALTER TABLE [dbo].[don_hang] CHECK CONSTRAINT [ck__thang_dh__don_hang]
GO

ALTER TABLE [dbo].[don_hang]  WITH CHECK ADD  CONSTRAINT [ck__thanh_tien__don_hang] CHECK  (([thanh_tien]>(-1)))
GO

ALTER TABLE [dbo].[don_hang] CHECK CONSTRAINT [ck__thanh_tien__don_hang]
GO

ALTER TABLE [dbo].[don_hang] ADD  CONSTRAINT [DF_don_hang_sp_ngay_dat]  DEFAULT ((1)) FOR [ngay_dh]
GO

ALTER TABLE [dbo].[don_hang] ADD  CONSTRAINT [DF_don_hang_sp_thang_dat]  DEFAULT ((1)) FOR [thang_dh]
GO

ALTER TABLE [dbo].[don_hang] ADD  CONSTRAINT [DF_don_hang_sp_nam_dat]  DEFAULT ((2014)) FOR [nam_dh]
GO

ALTER TABLE [dbo].[don_hang] ADD  CONSTRAINT [DF_don_hang_sp_thanh_tien]  DEFAULT ((0)) FOR [thanh_tien]
GO

ALTER TABLE [dbo].[don_hang] ADD  CONSTRAINT [DF_don_hang_sp_khoa]  DEFAULT ((0)) FOR [khoa]
GO

ALTER TABLE [dbo].[don_hang_chi_tiet]  WITH CHECK ADD  CONSTRAINT [FK_don_hang_chi_tiet_don_hang] FOREIGN KEY([id_dh])
REFERENCES [dbo].[don_hang] ([id_dh])
GO

ALTER TABLE [dbo].[don_hang_chi_tiet] CHECK CONSTRAINT [FK_don_hang_chi_tiet_don_hang]
GO

ALTER TABLE [dbo].[don_hang_chi_tiet]  WITH CHECK ADD  CONSTRAINT [FK_don_hang_chi_tiet_don_vi_tinh] FOREIGN KEY([id_dvt])
REFERENCES [dbo].[don_vi_tinh] ([id_dvt])
GO

ALTER TABLE [dbo].[don_hang_chi_tiet] CHECK CONSTRAINT [FK_don_hang_chi_tiet_don_vi_tinh]
GO

ALTER TABLE [dbo].[don_hang_chi_tiet]  WITH CHECK ADD  CONSTRAINT [FK_don_hang_chi_tiet_san_pham] FOREIGN KEY([id_sp])
REFERENCES [dbo].[san_pham] ([id_sp])
GO

ALTER TABLE [dbo].[don_hang_chi_tiet] CHECK CONSTRAINT [FK_don_hang_chi_tiet_san_pham]
GO

ALTER TABLE [dbo].[don_hang_chi_tiet]  WITH CHECK ADD  CONSTRAINT [ck__don_gia__don_hang_chi_tiet] CHECK  (([don_gia]>(-1)))
GO

ALTER TABLE [dbo].[don_hang_chi_tiet] CHECK CONSTRAINT [ck__don_gia__don_hang_chi_tiet]
GO

ALTER TABLE [dbo].[don_hang_chi_tiet]  WITH CHECK ADD  CONSTRAINT [ck__so_luong__don_hang_chi_tiet] CHECK  (([so_luong]>(-1)))
GO

ALTER TABLE [dbo].[don_hang_chi_tiet] CHECK CONSTRAINT [ck__so_luong__don_hang_chi_tiet]
GO

ALTER TABLE [dbo].[don_hang_chi_tiet] ADD  CONSTRAINT [DF_don_hang_sp_chi_tiet_don_gia]  DEFAULT ((0)) FOR [don_gia]
GO

ALTER TABLE [dbo].[don_hang_chi_tiet] ADD  CONSTRAINT [DF_don_hang_sp_chi_tiet_so_luong]  DEFAULT ((0)) FOR [so_luong]
GO

ALTER TABLE [dbo].[gio_hang]  WITH CHECK ADD  CONSTRAINT [FK_gio_hang_don_vi_tinh] FOREIGN KEY([id_dvt])
REFERENCES [dbo].[don_vi_tinh] ([id_dvt])
GO

ALTER TABLE [dbo].[gio_hang] CHECK CONSTRAINT [FK_gio_hang_don_vi_tinh]
GO

ALTER TABLE [dbo].[gio_hang]  WITH CHECK ADD  CONSTRAINT [FK_gio_hang_san_pham] FOREIGN KEY([id_sp])
REFERENCES [dbo].[san_pham] ([id_sp])
GO

ALTER TABLE [dbo].[gio_hang] CHECK CONSTRAINT [FK_gio_hang_san_pham]
GO

ALTER TABLE [dbo].[gio_hang]  WITH CHECK ADD  CONSTRAINT [FK_gio_hang_thanh_vien] FOREIGN KEY([id_tv])
REFERENCES [dbo].[thanh_vien] ([id_tv])
GO

ALTER TABLE [dbo].[gio_hang] CHECK CONSTRAINT [FK_gio_hang_thanh_vien]
GO

ALTER TABLE [dbo].[gio_hang]  WITH CHECK ADD  CONSTRAINT [ck__don_gia__gio_hang] CHECK  (([don_gia]>(-1)))
GO

ALTER TABLE [dbo].[gio_hang] CHECK CONSTRAINT [ck__don_gia__gio_hang]
GO

ALTER TABLE [dbo].[gio_hang]  WITH CHECK ADD  CONSTRAINT [ck__so_luong__gio_hang] CHECK  (([so_luong]>(-1)))
GO

ALTER TABLE [dbo].[gio_hang] CHECK CONSTRAINT [ck__so_luong__gio_hang]
GO

ALTER TABLE [dbo].[gio_hang] ADD  CONSTRAINT [DF_gio_hang_don_gia]  DEFAULT ((0)) FOR [don_gia]
GO

ALTER TABLE [dbo].[gio_hang] ADD  CONSTRAINT [DF_gio_hang_so_luong]  DEFAULT ((0)) FOR [so_luong]
GO

ALTER TABLE [dbo].[hoi_dap]  WITH CHECK ADD  CONSTRAINT [FK_hoi_dap_thanh_vien] FOREIGN KEY([id_tv])
REFERENCES [dbo].[thanh_vien] ([id_tv])
GO

ALTER TABLE [dbo].[hoi_dap] CHECK CONSTRAINT [FK_hoi_dap_thanh_vien]
GO

ALTER TABLE [dbo].[hoi_dap]  WITH CHECK ADD  CONSTRAINT [ck__nam_hd__hoi_dap] CHECK  (([nam_hd]>(2013)))
GO

ALTER TABLE [dbo].[hoi_dap] CHECK CONSTRAINT [ck__nam_hd__hoi_dap]
GO

ALTER TABLE [dbo].[hoi_dap]  WITH CHECK ADD  CONSTRAINT [ck__ngay_hd__hoi_dap] CHECK  (([ngay_hd]>(0) AND [ngay_hd]<(32)))
GO

ALTER TABLE [dbo].[hoi_dap] CHECK CONSTRAINT [ck__ngay_hd__hoi_dap]
GO

ALTER TABLE [dbo].[hoi_dap]  WITH CHECK ADD  CONSTRAINT [ck__thang_hd__hoi_dap] CHECK  (([thang_hd]>(0) AND [thang_hd]<(13)))
GO

ALTER TABLE [dbo].[hoi_dap] CHECK CONSTRAINT [ck__thang_hd__hoi_dap]
GO

ALTER TABLE [dbo].[hoi_dap] ADD  CONSTRAINT [DF_hoi_dap_ngay_hd]  DEFAULT ((1)) FOR [ngay_hd]
GO

ALTER TABLE [dbo].[hoi_dap] ADD  CONSTRAINT [DF_hoi_dap_thang_hd]  DEFAULT ((1)) FOR [thang_hd]
GO

ALTER TABLE [dbo].[hoi_dap] ADD  CONSTRAINT [DF_hoi_dap_nam_hd]  DEFAULT ((2014)) FOR [nam_hd]
GO

ALTER TABLE [dbo].[hoi_dap] ADD  CONSTRAINT [DF_hoi_dap_khoa]  DEFAULT ((0)) FOR [close_hd]
GO

ALTER TABLE [dbo].[hoi_dap] ADD  CONSTRAINT [DF_hoi_dap_public]  DEFAULT ((1)) FOR [chia_se]
GO

ALTER TABLE [dbo].[hoi_dap_tra_loi]  WITH CHECK ADD  CONSTRAINT [FK_hoi_dap_tra_loi_hoi_dap] FOREIGN KEY([id_hd])
REFERENCES [dbo].[hoi_dap] ([id_hd])
GO

ALTER TABLE [dbo].[hoi_dap_tra_loi] CHECK CONSTRAINT [FK_hoi_dap_tra_loi_hoi_dap]
GO

ALTER TABLE [dbo].[hoi_dap_tra_loi]  WITH CHECK ADD  CONSTRAINT [FK_hoi_dap_tra_loi_thanh_vien] FOREIGN KEY([id_tv])
REFERENCES [dbo].[thanh_vien] ([id_tv])
GO

ALTER TABLE [dbo].[hoi_dap_tra_loi] CHECK CONSTRAINT [FK_hoi_dap_tra_loi_thanh_vien]
GO

ALTER TABLE [dbo].[hoi_dap_tra_loi]  WITH CHECK ADD  CONSTRAINT [ck__nam_traloi__hoi_dap_tra_loi] CHECK  (([nam_traloi]>(2013)))
GO

ALTER TABLE [dbo].[hoi_dap_tra_loi] CHECK CONSTRAINT [ck__nam_traloi__hoi_dap_tra_loi]
GO

ALTER TABLE [dbo].[hoi_dap_tra_loi]  WITH CHECK ADD  CONSTRAINT [ck__ngay_traloi__hoi_dap_tra_loi] CHECK  (([ngay_traloi]>(0) AND [ngay_traloi]<(32)))
GO

ALTER TABLE [dbo].[hoi_dap_tra_loi] CHECK CONSTRAINT [ck__ngay_traloi__hoi_dap_tra_loi]
GO

ALTER TABLE [dbo].[hoi_dap_tra_loi]  WITH CHECK ADD  CONSTRAINT [ck__thang_traloi__hoi_dap_tra_loi] CHECK  (([thang_traloi]>(0) AND [thang_traloi]<(13)))
GO

ALTER TABLE [dbo].[hoi_dap_tra_loi] CHECK CONSTRAINT [ck__thang_traloi__hoi_dap_tra_loi]
GO

ALTER TABLE [dbo].[hoi_dap_tra_loi] ADD  CONSTRAINT [DF_hoi_dap_tra_loi_ngay_tra_loi]  DEFAULT ((1)) FOR [ngay_traloi]
GO

ALTER TABLE [dbo].[hoi_dap_tra_loi] ADD  CONSTRAINT [DF_hoi_dap_tra_loi_thang_tra_loi]  DEFAULT ((1)) FOR [thang_traloi]
GO

ALTER TABLE [dbo].[hoi_dap_tra_loi] ADD  CONSTRAINT [DF_hoi_dap_tra_loi_nam_tra_loi]  DEFAULT ((2014)) FOR [nam_traloi]
GO

ALTER TABLE [dbo].[quyen_han] ADD  CONSTRAINT [DF_quyen_han_khoa]  DEFAULT ((0)) FOR [khoa]
GO

ALTER TABLE [dbo].[san_pham]  WITH CHECK ADD  CONSTRAINT [FK_san_pham_danh_muc] FOREIGN KEY([id_dm])
REFERENCES [dbo].[danh_muc] ([id_dm])
GO

ALTER TABLE [dbo].[san_pham] CHECK CONSTRAINT [FK_san_pham_danh_muc]
GO

ALTER TABLE [dbo].[san_pham]  WITH CHECK ADD  CONSTRAINT [FK_san_pham_don_vi_tinh] FOREIGN KEY([id_dvt])
REFERENCES [dbo].[don_vi_tinh] ([id_dvt])
GO

ALTER TABLE [dbo].[san_pham] CHECK CONSTRAINT [FK_san_pham_don_vi_tinh]
GO

ALTER TABLE [dbo].[san_pham]  WITH CHECK ADD  CONSTRAINT [ck__don_gia__san_pham] CHECK  (([don_gia]>(-1)))
GO

ALTER TABLE [dbo].[san_pham] CHECK CONSTRAINT [ck__don_gia__san_pham]
GO

ALTER TABLE [dbo].[san_pham]  WITH CHECK ADD  CONSTRAINT [ck__luot_xem__san_pham] CHECK  (([luot_xem]>(-1)))
GO

ALTER TABLE [dbo].[san_pham] CHECK CONSTRAINT [ck__luot_xem__san_pham]
GO

ALTER TABLE [dbo].[san_pham]  WITH CHECK ADD  CONSTRAINT [ck__nam_sp__san_pham] CHECK  (([nam_sp]>(2013)))
GO

ALTER TABLE [dbo].[san_pham] CHECK CONSTRAINT [ck__nam_sp__san_pham]
GO

ALTER TABLE [dbo].[san_pham]  WITH CHECK ADD  CONSTRAINT [ck__ngay_sp__san_pham] CHECK  (([ngay_sp]>(0) AND [ngay_sp]<(32)))
GO

ALTER TABLE [dbo].[san_pham] CHECK CONSTRAINT [ck__ngay_sp__san_pham]
GO

ALTER TABLE [dbo].[san_pham]  WITH CHECK ADD  CONSTRAINT [ck__thang_sp__san_pham] CHECK  (([thang_sp]>(0) AND [thang_sp]<(13)))
GO

ALTER TABLE [dbo].[san_pham] CHECK CONSTRAINT [ck__thang_sp__san_pham]
GO

ALTER TABLE [dbo].[san_pham] ADD  CONSTRAINT [DF_san_pham_ngay_sp]  DEFAULT ((1)) FOR [ngay_sp]
GO

ALTER TABLE [dbo].[san_pham] ADD  CONSTRAINT [DF_san_pham_thang_sp]  DEFAULT ((1)) FOR [thang_sp]
GO

ALTER TABLE [dbo].[san_pham] ADD  CONSTRAINT [DF_san_pham_nam_sp]  DEFAULT ((2020)) FOR [nam_sp]
GO

ALTER TABLE [dbo].[san_pham] ADD  CONSTRAINT [DF_san_pham_don_gia]  DEFAULT ((0)) FOR [don_gia]
GO

ALTER TABLE [dbo].[san_pham] ADD  CONSTRAINT [DF_san_pham_luot_xem]  DEFAULT ((0)) FOR [luot_xem]
GO

ALTER TABLE [dbo].[thanh_vien]  WITH CHECK ADD  CONSTRAINT [FK_thanh_vien_quyen_han] FOREIGN KEY([ma_quyen])
REFERENCES [dbo].[quyen_han] ([ma_quyen])
GO

ALTER TABLE [dbo].[thanh_vien] CHECK CONSTRAINT [FK_thanh_vien_quyen_han]
GO

ALTER TABLE [dbo].[thanh_vien]  WITH CHECK ADD  CONSTRAINT [ck__account_name__thanh_vien] CHECK  ((NOT [account_name] like '%[^A-Z0-9]%'))
GO

ALTER TABLE [dbo].[thanh_vien] CHECK CONSTRAINT [ck__account_name__thanh_vien]
GO

ALTER TABLE [dbo].[thanh_vien]  WITH CHECK ADD  CONSTRAINT [ck__nam_sinh__thanh_vien] CHECK  (([nam_sinh]>(1914)))
GO

ALTER TABLE [dbo].[thanh_vien] CHECK CONSTRAINT [ck__nam_sinh__thanh_vien]
GO

ALTER TABLE [dbo].[thanh_vien]  WITH CHECK ADD  CONSTRAINT [ck__ngay_sinh__thanh_vien] CHECK  (([ngay_sinh]>(0) AND [ngay_sinh]<(32)))
GO

ALTER TABLE [dbo].[thanh_vien] CHECK CONSTRAINT [ck__ngay_sinh__thanh_vien]
GO

ALTER TABLE [dbo].[thanh_vien]  WITH CHECK ADD  CONSTRAINT [ck__sdt__thanh_vien] CHECK  (([sdt]>(-1)))
GO

ALTER TABLE [dbo].[thanh_vien] CHECK CONSTRAINT [ck__sdt__thanh_vien]
GO

ALTER TABLE [dbo].[thanh_vien]  WITH CHECK ADD  CONSTRAINT [ck__thang_sinh__thanh_vien] CHECK  (([thang_sinh]>(0) AND [thang_sinh]<(13)))
GO

ALTER TABLE [dbo].[thanh_vien] CHECK CONSTRAINT [ck__thang_sinh__thanh_vien]
GO

ALTER TABLE [dbo].[thanh_vien] ADD  CONSTRAINT [DF_khach_hang_ngay_sinh]  DEFAULT ((1)) FOR [ngay_sinh]
GO

ALTER TABLE [dbo].[thanh_vien] ADD  CONSTRAINT [DF_khach_hang_thang_sinh]  DEFAULT ((1)) FOR [thang_sinh]
GO

ALTER TABLE [dbo].[thanh_vien] ADD  CONSTRAINT [DF_khach_hang_nam_sinh]  DEFAULT ((1975)) FOR [nam_sinh]
GO

ALTER TABLE [dbo].[thanh_vien] ADD  CONSTRAINT [DF_khach_hang_sdt]  DEFAULT ((0)) FOR [sdt]
GO

ALTER TABLE [dbo].[thanh_vien] ADD  CONSTRAINT [DF_khach_hang_khoa]  DEFAULT ((0)) FOR [khoa]
GO

ALTER TABLE [dbo].[tin_tuc]  WITH CHECK ADD  CONSTRAINT [FK_tin_tuc_thanh_vien] FOREIGN KEY([id_tv])
REFERENCES [dbo].[thanh_vien] ([id_tv])
GO

ALTER TABLE [dbo].[tin_tuc] CHECK CONSTRAINT [FK_tin_tuc_thanh_vien]
GO

ALTER TABLE [dbo].[tin_tuc]  WITH CHECK ADD  CONSTRAINT [ck__luot_xem__tin_tuc] CHECK  (([luot_xem]>(-1)))
GO

ALTER TABLE [dbo].[tin_tuc] CHECK CONSTRAINT [ck__luot_xem__tin_tuc]
GO

ALTER TABLE [dbo].[tin_tuc]  WITH CHECK ADD  CONSTRAINT [ck__nam_tt__tin_tuc] CHECK  (([nam_tt]>(2013)))
GO

ALTER TABLE [dbo].[tin_tuc] CHECK CONSTRAINT [ck__nam_tt__tin_tuc]
GO

ALTER TABLE [dbo].[tin_tuc]  WITH CHECK ADD  CONSTRAINT [ck__ngay_tt__tin_tuc] CHECK  (([ngay_tt]>(0) AND [ngay_tt]<(32)))
GO

ALTER TABLE [dbo].[tin_tuc] CHECK CONSTRAINT [ck__ngay_tt__tin_tuc]
GO

ALTER TABLE [dbo].[tin_tuc]  WITH CHECK ADD  CONSTRAINT [ck__thang_tt__tin_tuc] CHECK  (([thang_tt]>(0) AND [thang_tt]<(13)))
GO

ALTER TABLE [dbo].[tin_tuc] CHECK CONSTRAINT [ck__thang_tt__tin_tuc]
GO

ALTER TABLE [dbo].[tin_tuc] ADD  CONSTRAINT [DF_tin_tuc_ngay_tt]  DEFAULT ((1)) FOR [ngay_tt]
GO

ALTER TABLE [dbo].[tin_tuc] ADD  CONSTRAINT [DF_tin_tuc_thang_tt]  DEFAULT ((1)) FOR [thang_tt]
GO

ALTER TABLE [dbo].[tin_tuc] ADD  CONSTRAINT [DF_tin_tuc_nam_tt]  DEFAULT ((2014)) FOR [nam_tt]
GO

ALTER TABLE [dbo].[tin_tuc] ADD  CONSTRAINT [DF_tin_tuc_luot_xem]  DEFAULT ((0)) FOR [luot_xem]
GO

ALTER TABLE [dbo].[tin_tuc] ADD  CONSTRAINT [DF_tin_tuc_ky_thuat]  DEFAULT ((0)) FOR [ky_thuat]
GO

ALTER TABLE [dbo].[tin_tuc] ADD  CONSTRAINT [DF_tin_tuc_khoa]  DEFAULT ((0)) FOR [luu_nhap]
GO

ALTER TABLE [dbo].[danh_muc]  WITH CHECK ADD  CONSTRAINT [FK_danh_muc_danh_muc] FOREIGN KEY([id_dm_cha])
REFERENCES [dbo].[danh_muc] ([id_dm])
GO

ALTER TABLE [dbo].[danh_muc]  WITH CHECK ADD  CONSTRAINT [CK_id_dm__id_dm_cha__danh_muc] CHECK  (([id_dm]<>[id_dm_cha]))
GO


