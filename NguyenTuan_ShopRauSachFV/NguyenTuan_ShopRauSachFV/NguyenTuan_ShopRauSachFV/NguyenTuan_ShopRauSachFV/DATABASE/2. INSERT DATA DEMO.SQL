USE [RAU_SACH_FV];

SET NOCOUNT ON;
SET XACT_ABORT ON;

GO

BEGIN TRANSACTION;
INSERT INTO [dbo].[cau_hinh]([ma_cau_hinh], [noi_dung])
SELECT N'dia_chi', N'BÌNH DƯƠNG' UNION ALL
SELECT N'email', N'RauSachFreshVegetables@gmail.Com' UNION ALL
SELECT N'sdt', N'0365335190' UNION ALL
SELECT N'ten_cua_hang', N'CỬA HÀNG RAU SẠCH FV'
COMMIT;
RAISERROR (N'[dbo].[cau_hinh]: INSERT BATCH: .....DONE!', 10, 1) WITH NOWAIT;
GO

SET IDENTITY_INSERT [dbo].[danh_muc] ON;

BEGIN TRANSACTION;
INSERT INTO [dbo].[danh_muc]([id_dm], [id_dm_cha], [ten_dm], [show_in_menu], [stt])
SELECT 1, NULL, N'DỊCH VỤ', 1, 1 UNION ALL
SELECT 2, NULL, N'RAU SẠCH ĂN LÁ', 1, 2 UNION ALL
SELECT 3, NULL, N'RAU SẠCH GIA VỊ', 1, 3 UNION ALL
SELECT 4, NULL, N'RAU SẠCH ĂN CỦ QUẢ', 1, 4 UNION ALL
SELECT 5, 1, N'GIÁ THỂ', 1, 1 UNION ALL
SELECT 6, 1, N'CHẬU NHỰA', 1, 2 UNION ALL
SELECT 7, 1, N'KHAY NHỰA', 1, 3 UNION ALL
SELECT 8, 1, N'KHAY XỐP', 1, 4 UNION ALL
SELECT 9, 1, N'PHÂN BÓN CAO CẤP', 1, 5 UNION ALL
SELECT 10, 1, N'DỤNG CỤ LÀM VƯỜN', 1, 6 UNION ALL
SELECT 11, 1, N'CHẬU - TÚI TREO TRỒNG RAU', 1, 7 UNION ALL
SELECT 12, 1, N'HẠT GIỐNG - RAU ĂN LÁ', 1, 8 UNION ALL
SELECT 13, 1, N'HẠT GIỐNG - RAU CỦ, QUẢ', 1, 9 UNION ALL
SELECT 14, 1, N'HẠT GIỐNG - HẠT RAU MẦM', 1, 10 UNION ALL
SELECT 15, 1, N'HẠT GIỐNG - RAU GIA VỊ', 1, 11 UNION ALL
SELECT 16, 1, N'HẠT GIỐNG - CÁC LOẠI HOA', 1, 12 UNION ALL
SELECT 17, 1, N'HẠT GIỐNG - CÂY ĂN TRÁI', 1, 13 UNION ALL
SELECT 18, 1, N'HẠT GIỐNG - KHÁC', 1, 14
COMMIT;
RAISERROR (N'[dbo].[danh_muc]: INSERT BATCH: .....DONE!', 10, 1) WITH NOWAIT;
GO

SET IDENTITY_INSERT [dbo].[danh_muc] OFF;

GO

SET IDENTITY_INSERT [dbo].[don_vi_tinh] ON;

BEGIN TRANSACTION;
INSERT INTO [dbo].[don_vi_tinh]([id_dvt], [ten_dvt])
SELECT 1, N'Kg' UNION ALL
SELECT 2, N'Bó' UNION ALL
SELECT 3, N'Quả' UNION ALL
SELECT 4, N'Túi' UNION ALL
SELECT 5, N'Hộp' UNION ALL
SELECT 6, N'100gr' UNION ALL
SELECT 7, N'Cái'
COMMIT;
RAISERROR (N'[dbo].[don_vi_tinh]: INSERT BATCH: .....DONE!', 10, 1) WITH NOWAIT;
GO

SET IDENTITY_INSERT [dbo].[don_vi_tinh] OFF;

GO

BEGIN TRANSACTION;
INSERT INTO [dbo].[quyen_han]([ma_quyen], [ten_quyen], [khoa])
SELECT N'Q001', N'QUẢN TRỊ PHẦN MỀM', 1 UNION ALL
SELECT N'Q002', N'QUẢN LÝ', 0 UNION ALL
SELECT N'Q003', N'KHÁCH HÀNG', 0
COMMIT;
RAISERROR (N'[dbo].[quyen_han]: INSERT BATCH: .....DONE!', 10, 1) WITH NOWAIT;
GO

SET IDENTITY_INSERT [dbo].[thanh_vien] ON;

BEGIN TRANSACTION;
INSERT INTO [dbo].[thanh_vien]([id_tv], [ma_quyen], [account_name], [email], [mat_khau], [ho_ten], [gioi_tinh], [ngay_sinh], [thang_sinh], [nam_sinh], [dia_chi], [sdt], [khoa], [ly_do_khoa])
SELECT 1, N'Q001', N'nguyentuan', N'nguyentuan5521@gmail.com', N'123456', N'Nguyễn Tuân', 0, 21, 09, 1999, N'BÌNH DƯƠNG', 0365335190, 0, NULL UNION ALL
SELECT 2, N'Q002', N'nhanvien1', N'nhanvien1@gmail.com', N'123456', N'Nhân Viên 1', 0, 12, 5, 1999, N'BÌNH DƯƠNG', 938848351, 0, NULL UNION ALL
SELECT 3, N'Q003', N'khachhang1', N'khachhang1@gmail.com', N'123456', N'Khách Hàng 1', 1, 2, 2, 1999, N'HÀ NỘI', 222222222, 0, NULL UNION ALL
SELECT 4, N'Q003', N'khachhang2', N'khachhang2@gmail.com', N'123456', N'Khách Hàng 2', 0, 3, 3, 1999, N'ĐÀ NẲNG', 333333333, 0, NULL UNION ALL
SELECT 5, N'Q003', N'khachhang3', N'khachhang3@gmail.com', N'123456', N'Khách Hàng 3', 1, 4, 4, 1999, N'HỒ CHÍ MINH', 444444444, 0, NULL UNION ALL
SELECT 6, N'Q003', N'khachhang4', N'khachhang4@gmail.com', N'123456', N'Khách Hàng 4', 1, 5, 5, 1999, N'CẦN THƠ', 555555555, 0, NULL
COMMIT;
RAISERROR (N'[dbo].[thanh_vien]: INSERT BATCH: .....DONE!', 10, 1) WITH NOWAIT;
GO

SET IDENTITY_INSERT [dbo].[thanh_vien] OFF;

GO

SET IDENTITY_INSERT [dbo].[don_hang] ON;

BEGIN TRANSACTION;
INSERT INTO [dbo].[don_hang]([id_dh], [id_tv], [ngay_dh], [thang_dh], [nam_dh], [thanh_tien], [khoa], [ly_do_khoa])
SELECT 1, 3, 2, 5, 2020, 260000, 0, NULL UNION ALL
SELECT 2, 3, 4, 6, 2020, 260000, 0, NULL
COMMIT;
RAISERROR (N'[dbo].[don_hang]: INSERT BATCH: .....DONE!', 10, 1) WITH NOWAIT;
GO

SET IDENTITY_INSERT [dbo].[don_hang] OFF;

SET IDENTITY_INSERT [dbo].[san_pham] ON;

BEGIN TRANSACTION;
INSERT INTO [dbo].[san_pham]([id_sp], [id_dm], [ten_sp], [gioi_thieu], [ngay_sp], [thang_sp], [nam_sp], [don_gia], [luot_xem], [id_dvt])
SELECT 1, 4, N'Súp lơ xanh', N'[b][img]/images_products/sup-lo-xanh.jpg[/img]

Giới thiệu sản phẩm[/b]

[i]Súp lơ có chứa các thành phần như: protein 3,5%; gluxit 4,9%; và nhiều khoáng chất, vitamin như: can-xi (26 mg%); phốtpho (51 mg%); sắt (1,4 mg%); natri (20 mg%); kali (349 mg%); betacaroten (40 mg%); vitamin B1 (0,11 mg%), vitamin C (70 mg%).[/i]

[b]Tác dụng của súp lơ xanh:[/b]
Súp lơ xanh thường giòn và dai hơn loại trắng nên có cảm giác ngon hơn, cũng rất tốt cho sức khỏe - giúp ngăn ngừa được nhiều căn bệnh như: tim mạch, ung thư tuyến tiền liệt, bàng quang, viêm loét dạ dày...

Súp lơ xanh còn có công dụng giúp cơ thể lâu lão hóa và tăng cường hệ miễn dịch ở người già. Nguyên tố vi lượng sắt và can-xi trong súp lơ xanh giúp các bé phát triển khỏe mạnh trong những năm đầu. Lượng vitamin C dồi dào giúp cơ thể bé hấp thu các dưỡng chất tốt hơn. Súp lơ xanh có đặc tính chống dị ứng, giảm thiểu nguy cơ trẻ mắc bệnh hen suyễn. Hàm lượng a-xít folic trong súp lơ xanh rất cao, giảm thiểu nguy cơ mắc dị tật bẩm sinh của thai nhi.

Nhưng cần biết, nếu chế biến súp lơ xanh ở nhiệt độ cao thì nhiều thành phần vitamin, đặc biệt là nhóm chất ngăn ngừa ung thư sẽ bị giảm, hoặc mất hết tác dụng.', 1, 10, 2016, 30000, 3, 1 UNION ALL
SELECT 2, 4, N'Củ cải đỏ', N'[img=500x500]/images_products/051_cucaido.jpg[/img]

[b]Giới thiệu sản phẩm[/b]
Củ cải đỏ (củ cải đường) không chỉ cung cấp cho bạn nhiều vitamin mà còn giúp giải độc tố một cách hiệu quả. Cùng tìm hiểu một số tác dụng của củ cải đỏ sau đây:

[b]1. Cung cấp nhiều vitamin[/b]

Củ cải đường chứa rất nhiều vitamin C, vitamin A (đặc biệt là ở lá), vitamin B9, sắt, kali, magiê, axit folic và betaine... Củ cải đường có tác dụng rất tốt cho tim mạch, giảm viêm nhiễm, đặc biệt tại các động mạch như chi, thái dương...

[b]2. Chống ung thư[/b]

Củ cải đường chứa nhiều chất chống oxy hóa, như beta-carotene và betacyanins. Các nhà khoa học đã chứng minh, các chất chống oxy hóa có thể ngăn chặn khối u phát triển và giám sát sự phát triển của tế bào bất thường. Nó đặc biệt có tác dụng trong việc phòng chống ung thư vú, tuyến tiền liệt, phổi, ruột kết và ung thư thận.

[b]3. Giúp giải độc tố[/b]

Củ cải đường giúp gan loại bỏ độc tố, đóng vai trò quan trọng việc hồi phục sức khỏe của con người. Một số hợp chất trong củ cải đường giúp trung hòa các độc tố và thải độc trong nước tiểu

[b]4. Tăng hiệu suất thể thao[/b]

Nước ép củ cải đường có thể giúp vận động viên cải thiện hiệu suất thi đấu của họ. Nhiều vận động viên ưu tú (phần lớn là các tay đua xe chuyên nghiệp) đã uống nước từ củ cải mỗi ngày.

[b]5. Kích thích tình dục[/b]

Từ thời La Mã cổ đại, người ta đã thường xuyên ăn củ cải đường để làm tăng hưng phấn, và có được sức khỏe tốt trong chuyện chăn gối. Khoa học hiện đại gần đây cũng đã tìm ra lý do củ cải đường chứa liều lượng của một hóa chất gọi là bo, đóng vai trò trực tiếp trong việc sản xuất hormone tình dục ở người.

[b]Một vài công thức chữa bệnh với củ cải đường[/b]

- Nước ép củ cải đường với cà rốt: Thức uống rất tốt cho việc giải độc cơ thể.

- Nước củ cải tươi hoặc ép (có thể thêm chút gừng): Chữa khản giọng, viêm nhiệt miệng.

- Bánh nhân củ cải (bột mì, bột ngọt, muối tiêu, thịt trộn, dầu cải, dầu vừng với củ cải để làm nhân bánh): Giảm ho, lợi khí.

- Củ cải, lê, gừng, sữa, mật ong... Cho hỗn hợp lên nồi khuấy, nguội bỏ ra hộp để mỗi ngày uống ngày 2 lần: Giảm ho suy nhược.

- Củ cải xào tỏi: Chống táo bón.

- Củ cải đường với 2 trái cam: Món salad tuyệt vời cho mùa lạnh.', 2, 10, 2020, 85000, 4, 1 UNION ALL
SELECT 3, 4, N'Su hào', N'[img]/images_products/1202224_su_hao.jpg[/img]

[b]Giới thiệu sản phẩm[/b]

Su hào là loại củ đã quá quen thuộc với người dân Việt. Đây là một loại rau củ giàu dinh dưỡng rất tốt cho sức khỏe. Sau đây là một vài công dụng của su hào

[b]Công dụng của su hào[/b]

[b]Giúp máu lưu thông tốt: [/b]Su hào là thực phẩm ít chất béo hoà tan và cholesterol. Điều đó có nghĩa rằng, nó rất tốt cho tim và hệ tuần hoàn máu. Chất béo hòa tan được biết đến là có hại cho cơ thể. Nếu ăn uống nhiều chất này sẽ làm tăng nồng độ cholesterol có hại ở trong máu. Từ đó dẫn đến nhiều bệnh ở tim như đau tim và đột quỵ. Hơn nữa, su hào giàu chất chống oxi hóa nên giúp tế bào ngăn ngừa sự tổn thương bởi các phân tử gốc tự do đi vào cơ thể qua hô hấp hoặc ăn uống. Những phân tử gốc này thường làm tăng nguy cơ đau tim và ung thư.

[b]Giúp hệ miễn dịch khỏe mạnh: [/b]Mùa đông hệ miễn dịch của chúng ta dễ bị tấn công do sự phát triển mạnh của vi rút, do vậy mà chúng ta dễ bị cảm cúm, ho, xổ mũi và mệt mỏi. Tuy nhiên, theo các chuyên gia, nếu bạn chăm ăn su hào vào mùa đông, nó sẽ giúp bạn tăng cường sức khỏe hệ miễn dịch. Lý do là trong su hào có chứa rất nhiều vitamin C. Một bát su hào sống có chứa lượng vitamin C nhiều hơn 1,4 lần so với nhu cầu cơ thể cần mỗi ngày. Hệ miễn dịch khỏe mạnh cũng giúp bạn chống lại các bệnh như tim mạch và ung thư. Ngoài ra vitamin C còn giúp cải thiện sự hấp thụ và phục hồi nguồn cung vitamin E cho cơ thể.

[b]Giúp hệ tiêu hóa khỏe mạnh: [/b]Su hào có hàm lượng chất xơ tuyệt vời cho cơ thể. Một bát su hào có chứa 5 gram chất xơ, bằng 19% lượng chất xơ cơ thể cần mỗi ngày. Chất xơ giúp hệ tiêu hóa hoạt động tốt bằng cách duy trì sức khỏe của ruột và ruột kết. Nó cũng có vai trò quan trọng trong việc duy trì lượng vi khuẩn có lợi trong ruột ở mức cân bằng. Tất cả những yếu tố trên giúp bạn giảm nguy cơ mắc các bệnh ở đường tiêu hóa, bệnh trĩ và ung thư ruột kết.

[b]Tăng cường chức năng thần kinh và cơ: [/b]Su hào cũng là loại củ giàu kali. Một bát su hào sống đáp ứng được khoảng 14% lượng kali cơ thể cần mỗi ngày. Kali là khoáng chất cần thiết cho sức khỏe của cơ thể cũng như chức năng thần kinh. Nó cũng hỗ trợ tích lũy carbohydrate- thành phần được sử dụng như là “nhiên liệu” cho cơ bắp. Nếu cơ thể nhận đầy đủ lượng kali cũng sẽ giúp bạn xử lý thông tin nhanh và không bị kích động khi gặp chuyện rắc rối.', 3, 10, 2016, 30000, 0, 1 UNION ALL
SELECT 4, 3, N'Củ hành tây', N'[img]/images_products/cach-dung-hanh-tay-lam-trong-lanh-khong-khi-loai-bo-ca-virut-cum.jpg[/img]

[b]Giới thiệu sản phẩm[/b]

Hành tây củ không chỉ là một loại thực phẩm mà nó còn là vị thuốc chữa bệnh rất công hiệu, tốt cho người mắc bệnh về đường hô hấp, bệnh hen suyễn, chứng mất ngủ và giảm cholesterol.

[b]Điểm nổi bật của Hành tây củ[/b]

- Không chất bảo quản

- Không thuốc trừ sâu

- Không chất kích thích tăng trưởng

- Không phân bón hóa học

- Không sử dụng giống biến đổi gen', 4, 10, 2020, 30000, 0, 1 UNION ALL
SELECT 5, 4, N'Dưa chuột', N'[img]/images_products/dua_chuot.jpg[/img]
[b]Giới thiệu sản phẩm[/b]

Dưa chuột là món ăn phổ biến và không hạn chế của nhiều dân tộc bởi họ tin vào những công dụng tuyệt vời của nó. Ngoài ra, dưa chuột cũng được xem như một loại “thần dược” của phái đẹp như làm đẹp, chữa bệnh. Cùng tìm hiểu những công dụng của dưa chuột

- Khi môi nứt nẻ: thái mỏng lát dưa chuột tươi ( càng nhiều nhựa càng tốt ) chà xát lên môi nhiều lần sẽ giúp môi mềm trở lại. Ngoài ra đắp lát dát dưa chuột lên da giúp giảm nếp nhăn, da bớt xù xì và tàn nhang.

- Cổ họng sưng và đau: Lấy một trái dưa chuột thật già, bỏ hạt, nhồi từ 10 đến 20 gr mang tiêu (mua ở tiệm thuốc Bắc) vào ruột dưa. Đem trái dưa đã nhồi phơi trong mát cho khô rồi cắt lát ngậm. Lưu ý phụ nữ có thai muốn áp dụng cách chữa trị này cần có sự tư vấn của bác sĩ.

- Làm hạ nhiệt, trị tiêu chảy: Dưa chuột non nấu với ít đường để uống.

- Trướng bụng, phù nề: Lấy trái dưa chuột già, bỏ hạt, cho thêm ít dấm ăn và nước vào xoong nấu nhừ. Món này ăn lúc đói. Lưu ý người bị bệnh dạ dày không nên ăn dưa chuột khi đói.

- Da bị mẩn đỏ: Dưa chuột băm hoặc xay lấy nước cốt bôi lên chỗ da nổi mẩn.', 5, 10, 2020, 30000, 0, 1 UNION ALL
SELECT 6, 3, N'Hành lá', N'[img]/images_products/hanh-la_1.jpg[/img]
[b]Giới thiệu sản phẩm[/b]

Hành lá đôi khi được gọi là hành ta (để phân biệt với hành tây tức Allium cepa) có danh pháp khoa học là Allium fistulosum thuộc họ hành (Alliaceae).
Hành ta không bao giờ có củ to và chắc như hành tây và cán hoa (tức lá hành) của nó là rỗng và mềm. Ngoài công dụng là một loại rau và gia vị trong việc nấu ăn thì chúng còn được dùng như là cây cảnh khi trồng thành bụi, thành khóm.', 6, 10, 2016, 7000, 0, 2 UNION ALL
SELECT 7, 3, N'Rau mùi', N'[img]/images_products/rau_mui_co_tac_dunggiaoducnetcn.jpg[/img]
[b]Giới thiệu sản phẩm[/b]
Rau mùi - được trồng bằng phương pháp hữu cơ không thuốc trừ sâu, không chất kích thích tăng trưởng, không phân bón hóa học, không sử dụng giống biến đổi gen

[b]Công dụng của rau mùi[/b]

Thường xuyên uống nước rau mùi sẽ giúp làm hạ cholesterol trong máu. Uống dịch ép từ rau mùi còn giúp bổ sung cho cơ thể một lượng lớn các vitamin như A, C, B1, B2 và chất sắt.

Ở VN, rau mùi tươi được dùng để làm gia vị nêm các món súp, điều chế các loại nước xốt, trang trí trên các món ăn cho đẹp vì màu xanh tươi và mùi thơm của lá. Tinh dầu của rau mùi dùng làm hương liệu trong ngành dược.', 7, 10, 2016, 7000, 0, 6 UNION ALL
SELECT 8, 4, N'Cà chua', N'[img]/images_products/ca-chua-giam-can.jpg[/img]
[b]Giới thiệu sản phẩm[/b]

Cà chua là một loại rau quả làm thực phẩm quen thuộc trong bữa ăn cũng như cuộc sống hàng ngày của mỗi chúng ta.

Cà chua có vị hơi chua và là một loại thực phẩm bổ dưỡng, giàu vitamin C và A, đặc biệt là giàu lycopeme tốt cho sức khỏe.

Cà chua được trồng và ăn trên khắp thế giới. Nó được sử dụng bằng nhiều cách khác nhau, bao gồm cả nguyên liệu trong món salad , và chế biến thành nước sốt cà chua hoặc súp cà chua . Cà chua chưa chín màu xanh lá cây cũng có thể được tẩm bột và chiên , được sử dụng để làm cho salsa, hoặc ngâm. Nước ép cà chua được bán như là một thức uống , và được sử dụng trong cocktail như Bloody Mary.

Cà chua có nhiều axit, giúp cho nó dễ dàng bảo quản và đóng hộp. Cà chua cũng được bảo quản bằng cách phơi dưới ánh nắng mặt trời.

Tuy nhiên hiện nay đã không ít người vì chạy theo lợi nhuận mà bỏ qua vấn đề sức khỏe của người tiêu dùng, họ sử dụng các chất bảo vệ thực vật trong quá trình chăm sóc, sử dụng chất kích thích để quả lớn nhau và chín mong hơn. Vì vậy là một người tiêu dùng thông minh bạn nên lựa chọn những cơ sở, cửa hàng uy tín để mua cà chua hay các sản phẩm rau củ quả cho gia đình và người thân. Mr. Sạch một trong những lựa chọn thông minh dành cho bạn.
Cà chua cũng như các loại thực phẩm khác tại Mr. Sạch được nuôi trồng theo phương pháp hữu cơ, không sử dụng thuốc bảo vệ thực phẩm, chất kích thích có hại cho cơ thể cũng như môi trường.', 8, 10, 2016, 30000, 1, 1 UNION ALL
SELECT 9, 2, N'Rau dền', N'[img]/images_products/rau-den-huu-co-1b7a1.jpg[/img]
[b]Giới thiệu sản phẩm[/b]

Rau dền cũng là một loại rau dân gian, rất dễ trồng, nó có ở hầu khắp các vùng vùng nông thôn. Rau dền có thể coi là một loại thực phẩm bổ dưỡng cho sức khỏe đặc biệt là rau rền đỏ rất bổ máu. Tuy nhiên hiện nay ở những khu chợ lớn có bán những mớ rau dền cọng và lá to trông tươi xanh nhưng rất có thể để có được mớ rau dền  như vậy họ đã sử dụng  huốc hoá học để chăm sóc vì vậy khi mua các bà nội trợ cũng nên để ý đến nguồn gốc của rau để mua được rau đúng chất lượng tránh bị xảy ra ngộ độc thực phẩm.

Rau dền được các hộ nông dân áp dụng đúng quy trình trồng rau hữu cơ. Khi ăn loại rau dền thường chúng ta không cảm nhận được vị đằm của rau, còn ăn rau hữu cơ chúng ta sẽ thấy vị ngọt và đằm, rất ngon.
Điểm nổi bật của rau dền hữu cơ:

- Không chất bảo quản

- Không thuốc trừ sâu

- Không chất kích thích tăng trưởng

- Không phân bón hóa học

- Không sử dụng giống biến đổi gen', 9, 10, 2020, 13000, 0, 4 UNION ALL
SELECT 10, 2, N'Xà lách', N'[b][img]/images_products/xa-lach-18f73.jpg[/img]
[/b]
[b]Giới thiệu sản phẩm[/b]

Rau Xà lách hay còn gọi là rau diếp là một cây ôn đới thuộc họ Cúc. Xà lách thường được trồng làm rau ăn lá, đặc biệt trong món salad, bánh sandwich, hamburger và nhiều món ăn khác, hoặc nấu chín, như trong các món ăn Trung Quốc.

Thành phần hóa học: Có lactucarium, lactucorin, lactucin, acid lactucic, asparagin, hyoscyamin, chlorophylle, vitamin A, B, C, D, E; các chất khoáng Fe, Ca, P, I, Mn, Zn, Cu, Na, Cl, K, Co, As, phosphat, sulfat, sterol, caroten.

Tính vị, tác dụng: Vị ngọt đắng, tính mát; có tác dụng giải nhiệt, lọc máu, khai vị (vào đầu bữa ăn, nó kích thích các tuyến tiêu hóa), cung cấp chất khoáng, giảm đau, gây ngủ, làm dịu, chống ho, chống đái đường, làm mềm, lợi sữa, dẫn mật, chống thối.

Công dụng, chỉ định và phối hợp: Xà lách được chỉ định dùng làm thuốc trong các trường hợp thần kinh dễ bị kích thích, suy nhược tâm thần, đánh trống ngực, co giật nội tạng, chứng đau dạ dày, di mộng tinh, kích thích sinh lý, đau do lậu, mất ngủ, thiếu chất khoáng, ho, ho gà, ho thần kinh, suyễn, đái đường, thống phong, tạng khớp, bệnh sởi, viêm thận, hành kinh đau bụng, vàng da, sung huyết gan, táo bón.', 10, 10, 2016, 17000, 0, 1 UNION ALL
SELECT 11, 2, N'Bắp cải', N'[img]/images_products/bap-cai-1b13a.jpg[/img]
[b]Giới thiệu sản phẩm[/b]
Bắp Cải được trồng theo phương pháp hữu cơ không sử dụng các chất kích thích, chất bảo vệ thực vật, đã qua kiểm tra chất lượng nghiêm ngặt đảm bảo vệ sinh an toàn thực phẩm.

Bắp cải hay cải bắp là một loại rau chủ lực trong họ Cải (còn gọi là họ Thập tự - Brassicaceae/Cruciferae), phát sinh từ vùng Địa Trung Hải. Nó là cây thân thảo, sống hai năm, và là một thực vật có hoa thuộc nhóm hai lá mầm với các lá tạo thành một cụm đặc hình gần như hình cầu đặc trưng.

Công dụng của cải bắp: Trong cải bắp chứa nhiều biotine, hay còn gọi là vitamin H, vốn được mệnh danh là "vitamin của sắc đẹp". Biotine có tác dụng rất tốt, tạo điều kiện cho sự phát triển của móng tay, tóc; Làm đẹp da và có lợi cho hệ thống thần kinh, tủy xương, giúp giảm đau cơ. Vì thế, hãy thường xuyên bổ sung biotine bằng cách ăn rau cải bắp vài lần trong tuần. Có thể chế biến cải bắp thành nhiều món như luộc, xào, làm dưa góp, làm món thịt cuốn cải bắp..

Cải bắp ngoài là món ăn ngon ra còn có tác dụng chữa được nhiều bệnh như: phòng bệnh ung thư vú ở phụ nữ,nước cải bắp tươi chữa bệnh loét da.', 11, 10, 2016, 30000, 0, 1 UNION ALL
SELECT 12, 1, N'Giá thể (Đất sạch)', N'[b]Giá thể (Đất sạch)[/b]

Giá thể Là hỗn hợp đất dinh dưỡng hữu cơ để trồng rau ăn quả, cây hoa, giúp cây phát triển cân đối, phân cành sớm, khỏe, ra hoa đậu trái nhiều, màu sắc đẹp

[img]/images_products/dat%20sach.jpg[/img]', 12, 10, 2020, 50000, 0, 4 UNION ALL
SELECT 13, 1, N'Đất phù sa (đất dinh dưỡng trồng cây)', N'[b]Đất phù sa (đất dinh dưỡng trồng cây)[/b]

Giá thể Là hỗn hợp đất dinh dưỡng hữu cơ để trồng rau ăn quả, cây hoa, giúp cây phát triển cân đối, phân cành sớm, khỏe, ra hoa đậu trái nhiều, màu sắc đẹp

[img]/images_products/dat%20phu%20sa.jpg[/img]', 13, 10, 2020, 50000, 0, 4 UNION ALL
SELECT 14, 1, N'Giá thể hữu cơ từ bụi xơ dừa', N'[b]Giá thể hữu cơ từ bụi xơ dừa: [/b]chuyên dùng để trồng tất cả các lọai rau mầm

[img]/images_products/bui xo dua.jpg[/img]', 14, 10, 2020, 50000, 0, 4 UNION ALL
SELECT 15, 1, N'Giá thể hữu cơ để trồng giá, ươm cây', N'[b]Giá thể hữu cơ để trồng giá, ươm cây, trồng thủy canh[/b] dền đen dền trắng, chỉ xơ dừa 
[img]/images_products/chi_so_dua.jpg[/img]', 15, 10, 2020, 50000, 0, 4 UNION ALL
SELECT 16, 1, N'Phân hữu cơ bón gốc', N'[b]Phân hữu cơ bón gốc[/b]
[img]/images_products/phan_bon_goc.jpg[/img]', 16, 10, 2020, 100000, 0, 5 UNION ALL
SELECT 17, 1, N'Bình tưới phun', N'[b]Bình tưới phun[/b]
[img]/images_products/binh_tuoi_phun.jpg[/img]', 17, 10, 2020, 200000, 0, 7 UNION ALL
SELECT 18, 2, N'Rau cải Ngồng', N'[img]/images_products/rau-cai-ngong-25949.jpg[/img]
[b]Giới thiệu sản phẩm[/b]

Cải ngồng có chứa nhiều bêta caroten, sinh tố nhóm B và C, có khả năng ức chế hình thành sắc tố melanin, giúp da sáng đẹp. Cải ngồng còn có tác dụng làm sáng mắt nhờ vitamin A có trong rau cải
Chi tiết sản phẩm rau cải Ngồng Mr Sạch

[b]Thành phần: [/b]Rau cải Ngồng  sạch được trồng bằng phương pháp hữu cơ không sử dụng hóa chất.

[b]Cách sử dụng: [/b]Dùng để chế biến món ăn về rau củ quả

[b]Cách bảo quản: [/b] Bảo quản trong ngăn mát tủ lạnh hoăc nhiệt độ từ 1 - 4 độ c', 18, 10, 2020, 13000, 2, 4 UNION ALL
SELECT 19, 2, N'Rau cải chíp', N'[img]/images_products/rau-cai-chip-2556b.jpg[/img]
[b]Giới thiệu sản phẩm[/b]

Rau cải chíp có tính mát, vị ngọt, có tác dụng tán hàn tiêu thũng, thanh nhiệt giải độc… Cải chíp có tác dụng hỗ trợ điều trị đối với các bệnh tăng huyết áp, hở van tim, viêm thận, chảy máu lợi, hoại huyết và bệnh về huyết quản não.
Chi tiết sản phẩm rau cải chíp tại Mr Sạch

[b]Thành phần: [/b]Rau cải chíp sạch được trồng bằng phương pháp hữu cơ không sử dụng hóa chất.

[b]Cách sử dụng: [/b]Dùng để chế biến món ăn về rau củ quả

[b]Cách bảo quản: [/b]Bảo quản trong ngăn mát tủ lạnh hoăc nhiệt độ từ 1 - 4 độ c', 19, 10, 2020, 13000, 5, 4 UNION ALL
SELECT 20, 4, N'Quả mướp hương', N'[img]/images_products/muop-huong.jpg[/img]
[b]Giới thiệu sản phẩm[/b]

Mướp hương được trồng ở khắp nơi để lấy quả làm thức ăn và các bộ phận khác dùng để chữa bệnh. Gọi là mướp hương là do quả mướp thoang thoảng hương nếp như mùi lá dứa thơm.

Theo đông y, mướp hương vị ngọt, tính bình; có tác dụng làm điều kinh (phụ nữ kinh nguyệt quá nhiều nên ăn mướp xào với tôm cả vỏ), ngừng bạch đới, bình can ngưng phong, làm mát, nhuận da, thông đại tiểu tiện; thường dùng để chữa các chứng bệnh như: Sốt cao phiền khát, viêm họng, viêm phế quản, trĩ, bạch đới, viêm đường tiết niệu, sản phụ sữa không thông, táo bón (trái mướp nấu canh ăn).

Quả mướp nấu canh ăn hằng ngày có tác dụng nhuận tràng và làm dịu đau (chính là do chất nhày chứa với hàm lượng cao trong quả). Quả mướp non ninh với chân giò hoặc móng giò lợn là thuốc tăng tiết sữa và làm máu lưu thông; xơ mướp được dùng trong y học cổ truyền với tên thuốc là ty qua lạc, có vị ngọt dịu, tính bình, có tác dụng cầm máu, thông kinh lạc, chống co thắt, thúc sởi, lợi tiểu.

[b]Chi tiết sản phẩm mướp hương[/b]

[b]Thành phần: [/b]Mướp hương được trồng theo phương pháp hữu cơ không sử dụng thuốc bảo vệ thực vật

[b]Cách sử dụng: [/b]Dùng để chế biến các món ăn như mướp xào giá, mướp xào mề gà,..

[b]Cách bảo quản: [/b]Để nơi thoáng mát, tránh ảnh sáng mặt trời', 20, 10, 2020, 30000, 0, 1 UNION ALL
SELECT 21, 2, N'Bắp cải trắng', N'[img]/images_products/rau-bap-cao.jpg[/img]
[b]Giới thiệu sản phẩm[/b]

Bắp cải được các bà nội trợ biến hóa thành nhiều món ăn ngon. Cải bắp ngoài là một thực phẩm bổ dưỡng mà còn có tác dụng chữa được nhiều bệnh như: phòng bệnh ung thư vú ở phụ nữ,nước cải bắp tươi chữa bệnh loét da.

[b]Chi tiết sản phẩm bắp cải trắng[/b]

[b]Thành phần: [/b]bắp cải trắng  sạch được trồng bằng phương pháp hữu cơ không sử dụng hóa chất.

[b]Cách sử dụng:[/b] Dùng để chế biến món ăn về rau củ quả đặc biệt dùng để nấu lẩu

[b]Cách bảo quản: [/b] Bảo quản trong ngăn mát tủ lạnh hoăc nhiệt độ từ 1 - 4 độ c', 21, 10, 2020, 30000, 0, 1
COMMIT;
RAISERROR (N'[dbo].[san_pham]: INSERT BATCH: .....DONE!', 10, 1) WITH NOWAIT;
GO

SET IDENTITY_INSERT [dbo].[san_pham] OFF;

SET IDENTITY_INSERT [dbo].[tin_tuc] ON;

BEGIN TRANSACTION;
INSERT INTO [dbo].[tin_tuc]([id_tt], [id_tv], [tieu_de], [noi_dung], [ngay_tt], [thang_tt], [nam_tt], [luot_xem], [ky_thuat], [luu_nhap])
SELECT 1, 2, N'GIỚI THIỆU', N'[b]Giới thiệu[/b]

Cửa hàng [b]RAU SẠCH FRESH VEGETABLES[/b] xin gửi tới quý khách hàng lời chào trân trọng và lời chúc tốt đẹp nhất!

Hiện nay, chúng tôi đã và đang phục vụ gần 1.500 hộ gia đình, 15 bệnh viện, 450 công ty, tổ chức lớn ở Bình Dương. Chính vì vậy, chúng tôi luôn hiểu phải kinh doanh sản phẩm như thế nào và dịch vụ ra sao để đáp ứng được ngay cả những khách hàng khó tính nhất. Chúng tôi cam kết mang tới khách hàng kính yêu những sản phẩm chất lượng tốt nhất, giá thành tốt nhất, chất lượng dịch vụ hoàn hảo nhất.

Năm 2005 Trung ương hội nông dân Việt Nam “VNFU” kết hợp với tổ chức phát triển nông nghiệp Châu Á – Đan Mạch “ADDA”, thực hiện dự án “phát triển khuôn khổ cho sản xuất và marketing nông nghiệp hữu cơ (NNHC) tại Việt Nam” đến nay đã thu được kết quả rất khả quan về chất lượng sản phẩm cũng như chất lượng con người lao động. Trên thế giới nông nghiệp hữu cơ phục vụ tầng lớp thượng lưu vì nó tạo ra sản phẩm cho giá trị dinh dưỡng rất cao, cực sạch, sử dụng liên tục làm tăng sức khỏe, lại bảo vệ môi trường sống cho con người và các sinh vật, nông nghiệp hữu cơ phát triển trên nguyên tắc đa dạng sinh học, luôn canh gối vụ, các sinh vật phát triển tự nhiên, sử dụng phân hữu cơ ngâm ủ theo đúng kỹ thuật, hệ động thực vật khỏe mạnh có sức đề kháng và chống chịu tốt, tuyệt đối không sử dụng thuốc bảo vệ thực vật, phân bón hoá học. Để làm được NNHC Người nông dân được tập huấn kỹ và nghiêm túc về sản xuất NNHC (khoảng 17 tuần một lớp, do cán bộ dự án đào tạo), đất và nước nuôi, trồng phải đảm bảo xét nghiệm hoàn toàn sạch đạt tiêu chuẩn NNHC của IFOAM, khu nuôi trồng đảm bảo cách ly với môi trường bên ngoài, xa nguồn ô nhiễm, sản phẩm hữu cơ được nuôi, trồng sau một thời gian dài (sau khi cải tạo đất, môi trường) mới có thể được coi là sản phẩm hữu cơ  theo tiêu chuẩn hữu cơ chung của thế giới.

Chúng tôi mong rằng sự thành tâm, hiểu biết và cố gắng không mệt mỏi của chúng tôi sẽ đáp ứng được những yêu cầu khắt khe nhất của khách hàng.

[b]Địa chỉ: [/b]Bình Dương, Việt Nam

[b]Hình thức thanh toán:[/b] Trực tiếp tại cửa hàng, hoặc giao hàng tận nơi và thu tiền.', 1, 10, 2020, 0, 0, 0 UNION ALL
SELECT 2, 2, N'CAM KẾT CHẤT LƯỢNG', N'[b]Định nghĩa tiêu chuẩn VietGAP[/b]

Tiêu chuẩn VietGAPVietGAP hay còn gọi là thực hành sản xuất nông nghiệp tốt cho rau quả tươi của Việt Nam (Vietnamese Good Agricultural Practices) là những nguyên tắc, trình tự, thủ tục hướng dẫn tổ chức, cá nhân sản xuất, thu hoạch, sơ chế bảo đảm an toàn, nâng cao chất lượng sản phẩm, đảm bảo phúc lợi xã hội, sức khỏe người sản xuất và người tiêu dùng, bảo vệ môi trường và truy nguyên nguồn gốc sản phẩm. VietGAP cho rau, quả tươi an toàn dựa trên cơ sở ASEAN GAP, EUREPGAP/GLOBALGAP và FRESHCARE, nhằm tạo điều kiện thuận lợi cho rau, quả Việt Nam tham gia thị trường khu vực ASEAN và thế giới, hướng tới sản xuất nông nghiệp bền vững.

[b]Định nghĩa nguyên tắc HACCP[/b]

Nguyên tắc HACCPHACCP (viết tắt của Hazard Analysis and Critical Control Points, được dịch ra tiếng Việt là Phân tích mối nguy và điểm kiểm soát tới hạn), là những nguyên tắc được sử dụng trong việc thiết lập hệ thống quản lý an toàn thực phẩm. HACCP được nhiều nước trên thế giới quy định bắt buộc áp dụng trong quá trình sản xuất, chế biến thực phẩm. Uỷ ban Tiêu chuẩn Thực phẩm quốc tế (CODEX) cũng khuyến cáo việc nên áp dụng HACCP kết hợp với việc duy trì điều kiện sản xuất (GMP) để nâng cao hiệu quả của việc đảm bảo chất lượng vệ sinh an toàn thực phẩm. HACCP được giới thiệu trong tiêu chuẩn của CODEX mang số hiệu CAC/RCP 1-1969, Rev.4-2003, và tiêu chuẩn quốc gia của Việt Nam tương đương là TCVN 5603:2008.

[b]7 nguyên tắc của HACCP[/b]
- Nhận diện mối nguy;
- Xác định điểm kiểm soát tới hạn (CCP - Critical Control Points);
- Xác định giới hạn tới hạn cho mỗi CCP;
- Thiết lập thủ tục giám sát CCP;
- Thiết lập kế hoạch hành động khắc phục khi giới hạn tới hạn bị phá vỡ;
- Xây dựng thủ tục thẩm tra hệ thống HACCP;
- Thiết lập thủ tục lưu trữ hồ sơ HACCP.

[b]Nông nghiệp Hữu cơ[/b]

Nông nghiệp hữu cơ là một hình thức nông nghiệp tránh hoặc loại bỏ phần lớn việc sử dụng phân bón tổng hợp, thuốc trừ sâu, các chất điều tiết tăng trưởng của cây trồng, và các chất phụ gia trong thức ăn gia súc.

Nông nghiệp hữu cơ, theo định nghĩa của Liên Hiệp Quốc, là hệ thống canh tác và chăn nuôi tự nhiên, không sử dụng hóa chất làm phân bón và thuốc trừ sâu, giúp giảm thiểu ô nhiễm, bảo đảm sức khỏe cho người và vật nuôi.

Các nông dân canh tác theo hình thức nông nghiệp hữu cơ dựa tối đa vào việc quay vòng mùa vụ, các phần thừa sau thu hoạch, phân động vật và việc canh tác cơ giới để duy trì năng suất đất để cung cấp các chất dinh dưỡng cho cây trồng, và kiểm soát cỏ, côn trùng và các loại sâu bệnh khác.

Mục đích hàng đầu của nông nghiệp hữu cơ là tối đa hóa sức khỏe và năng suất của các cộng đồng độc lập về đời sống đất đai, cây trồng, vật nuôi và con người.

[b]Nông nghiệp hữu cơ được dựa trên:[/b]

+ Các nguyên tắc về sức khỏe.
+ Các nguyên tắc về sinh thái.
+ Các nguyên tắc về sự công bằng.
+ Các nguyên tắc về sự chăm sóc.

[b]Sự khác nhau giữa sản phẩm nông nghiệp hữu cơ và sản phẩm sạch, an toàn khác là gì?[/b]

Sự khác biệt rõ nhất giữa các loại sản phẩm hữu cơ với sản phẩm sạch, an toàn khác là quy trình sản xuất: Sản xuất các sản phẩm hữu cơ không sử dụng thuốc trừ sâu và phân hoá học, nguồn thức ăn trong chăn nuôi là nguồn thức ăn tự nhiên. Trong khi quy trình sản xuất rau quả và sản phẩm nông nghiệp sạch, an toàn vẫn sử dụng một số lượng nhất định thuốc trừ sâu và phân bón hoá học, thức ăn tăng trọng và các chất kích thích trong chăn nuôi.

[b]Định nghĩa Thịt / Gia cầm / Thịt chế biến[/b]

Thịt của động vật có vú do săn bắn và nuôi được dùng làm thức ăn cho con người ví dụ như thịt bò, thịt heo ...

[b]Các nhóm thuộc họ thịt[/b]

+ Thịt bò
+ Thịt heo
+ Thịt dê
+ Thịt cừu
+ Thịt ngựa
+ Thịt thú săn
+ Thịt thỏ
+ Thịt nhập khẩu

[b]Thịt gia cầm là các loài chim hoang dã và đã thuần hóa được nuôi để đẻ trứng và cho thịt[/b]

[b]Các nhóm thuộc họ gia cầm[/b]

+ Gà giò
+ Gà Tây
+ Gà Nhật
+ Gà rừng
+ Vịt
+ Ngỗng
+ Vịt

[b]Thịt chế biến là những sản phẩm được chế biến từ thịt, mỡ và các nguyên liệu khác, chủ yếu là để bảo quản chúng được lâu hơn theo quy trình chăn nuôi.[/b]

[b]Các nhóm thuộc thịt chế biến[/b]

+ Xúc xích khô
+ Xích xích đã nấu chín
+ Bơ lát và thịt cuộn
+ Thịt khô hoặc xông khói
+ Thịt nấu chín
+ Xúc xích tươi', 2, 10, 2020, 0, 0, 0 UNION ALL
SELECT 3, 2, N'CHÍNH SÁCH BÁN HÀNG', N'[b]Điều khoản sử dụng[/b]

- Xác lập một thỏa thuận giữa NGƯỜI SỬ DỤNG website và CỬA HÀNG RAU SẠCH FRESH VEGETABLES
- Trách nhiệm của Công ty là đảm bảo các tính hợp pháp cho các nội dung trên Website.
- Trách nhiệm của NGƯỜI SỬ DỤNG là tuân thủ các điều khoản và điều kiện sử dụng theo quy định của Công ty khi truy cập và/hoặc sử dụng bất kỳ tiện ích nào từ website Chúng tôi.
- Chúng tôi chịu trách nhiệm đảm bảo tính hợp pháp của việc cung cấp Website đến NGƯỜI SỬ DỤNG, các thông tin, hình ảnh và toàn bộ các nội dung được đưa lên Website không vi phạm các quy định pháp luật cấm, các quy định về thuần phong mỹ tục .
- NGƯỜI SỬ DỤNG đồng ý có trách nhiệm tuân thủ các điều kiện, điều khoản sử dụng website như được nêu bên dưới;
- NGƯỜI SỬ DỤNG đồng ý có trách nhiệm đọc cẩn thận các điều khoản sử dụng và cam kết ràng buộc. Trường hợp không đồng ý NGƯỜI SỬ DỤNG chấm dứt việc sử dụng;
- Điều khoản sử dụng có hiệu lực kể từ thời điểm NGƯỜI SỬ DỤNG bắt đầu truy cập vào website
 
[b]BẢN QUYỀN VÀ THƯƠNG HIỆU[/b]

Tuyên bố các quyền Sở hữu trí tuệ của Công ty đối với các đối tượng quyền thể hiện trên website
- Trang web này và tất cả nội dung của trang web, bao gồm nhưng không giới hạn các văn bản, thiết kế, đồ họa, giao diện, hình ảnh và mã code đều thuộc bản quyền của Chúng tôi hoặc bên thứ ba cấp phép cho Chúng tôi. Bản quyền của Chúng tôi được thể hiện trên trang web bằng dòng chữ tiếng Anh "© Copyright - All rights reserved.". 
- Bất kỳ nội dung nào thuộc trang Web này về nhãn hiệu, tên thương mại, KDCN... đều thuộc quyền sở hữu của Chúng tôi và được bảo hộ theo quy định của Luật Sở hữu trí tuệ Việt Nam và các văn bản liên quan.
- Mọi hành vi sao chép, sửa đổi, phân phối, xuất bản, lưu thông … vì mục đích thương mại mà không được sự cho phép trước bằng văn bản của Chúng tôi là xâm phạm quyền của Chúng tôi. Chúng tôi có quyền yêu cầu NGƯỜI SỬ DỤNG chấm dứt việc sử dụng và bồi thường thiệt hại (nếu có).

[b]NỘI DUNG

Mục đích:[/b]
 
-Các thông tin được đăng tải trên website với mục đích làm lợi cho người tiêu dùng (cung cấp thông tin về công ty, cổ đông sản phẩm và những thông tin dinh dưỡng, chương trình khuyến mại của công ty…)
- Chúng tôi không có trách nhiệm đưa ra những lời khuyên hay dịch vụ về y tế. Các thông tin được hiển thị trên trang web này cũng không nhằm mục đích khám hay chữa bệnh. Qu‎ý vị nên đi khám bác sĩ để có những dịch vụ tư vấn chính xác, bao gồm cả những tư vấn về chế độ ăn hay chương trình tập luyện.
 
[b]Quyền và trách nhiệm của Chúng tôi:[/b]
 
- Chúng tôi có quyền thay đổi và/hoặc chấm dứt các nội dung, tính năng của website mà không cần thông báo trước với NGƯỜI SỬ DỤNG, NGƯỜI SỬ DỤNG có trách nhiệm thường xuyên cập nhật các điều kiện và điều khoản trong thông báo này.
- Chúng tôi có quyền thực hiện các biện pháp an ninh để bảo vệ và chống các truy cập trái phép hoặc sửa đổi trái phép, tiết lộ hoặc phá hủy thông tin.
- Chúng tôi không tuyên bố hoặc không có trách nhiệm chứng thực tính chính xác hay độ tin cậy của bất kỳ lời khuyên, ý kiến, phát biểu hoặc các thông tin từ các nguồn khác hoặc từ bất kỳ NGƯỜI SỬ DỤNG nào tại Website này.
- Khi sử dụng các trang web, thông tin sẽ được truyền qua một phương tiện nằm ngoài sự kiểm soát Chúng tôi. Theo đó, Chúng tôi không chịu trách nhiệm cho hoặc liên quan đến bất kỳ sự chậm trễ, thất bại, bị gián đoạn của bất kỳ dữ liệu hoặc các thông tin khác được truyền trong kết nối với việc sử dụng của trang web.
- Thông tin cá nhân như địa chỉ, email của NGƯỜI SỬ DỤNG có thể được sử dụng nội bộ để duy trì hồ sơ thành viên, cho mục đích tiếp thị và cảnh báo các khách hàng/thành viên các dịch vụ bổ sung. Số điện thoại cũng có thể được sử dụng bởi Chúng tôi khi các câu hỏi liên quan đến sản phẩm được đề nghị giải đáp. Chúng tôi không tiết lộ bất kỳ thông tin về người dùng cá nhân cho bên thứ ba ngoại trừ việc tuân theo pháp luật hoặc các quy trình pháp lý hợp lệ
- Tất cả các thông tin, tài liệu không mang tính chất riêng tư được NGƯỜI SỬ DỤNG đưa lên trang trang web đều không được xem là thông tin mật, và Chúng tôi được phép toàn quyền sử dụng hay chuyển tải cho bất kỳ mục đích nào. Đặc biệt, Chúng tôi được phép sử dụng bất kỳ ‎ý tưởng hay khái niệm nào mà NGƯỜI SỬ DỤNG đã đưa lên trang web cho tất cả các mục đích không giới hạn, bao gồm cả việc phát triển, sản xuất, quảng cáo hay tiếp thị sản phẩm. Chúng tôi không có trách nhiệm phải bồi thường hay thưởng cho người cung cấp.
- NGƯỜI SỬ DỤNG phải ‎ý thức và bảo đảm những thông tin, tài liệu mà NGƯỜI SỬ DỤNG gửi phải nằm trong quyền hạn sử dụng của NGƯỜI SỬ DỤNG ; điều đó có nghĩa Chúng tôi sẽ không vi phạm bất cứ quyền lợi nào của bên thứ ba.
- Chúng tôi không nhất thiết phải chấp nhận tất cả tài liệu NGƯỜI SỬ DỤNG gửi.
 
[b]Quyền và trách nhiệm của NGƯỜI SỬ DỤNG:[/b]
 
- Chúng tôi cho phép NGƯỜI SỬ DỤNG xem, chiết xuất thông tin trên webisite (in, tải,…) hoặc chia sẻ cho người khác nhưng chỉ cho mục đích sử dụng cá nhân và phi thương mại với điều kiện phải trích dẫn thông báo bản quyền sau đây: "© Copyright”
- NGƯỜI SỬ DỤNG đảm bảo tuân theo pháp luật và các quy định liên quan đến việc sử dụng website; không can thiệp, gây ảnh hưởng đến việc sử dụng website của những NGƯỜI SỬ DỤNG khác; không can thiệp vào hoạt động và quản lý website của Chúng tôi.
- NGƯỜI SỬ DỤNG nhận thức rõ và chấp nhận rằng Chúng tôi/các đơn vị trực thuộc/ NPP /nhân viên Chúng tôi không chịu trách nhiệm đối với bất kỳ tổn thất, thiệt hại, chi phí phát sinh từ bất kỳ quyết định nào của NGƯỜI SỬ DỤNG khi sử dụng bất kỳ thông tin nào trên website với bất kỳ nguyên nhân gì.
- Nếu NGƯỜI SỬ DỤNG không bằng lòng với bất kỳ thông tin nào trên Website hoặc với bất kỳ điều khoản và điều kiện sử dụng thông tin trên Website này thì phương thức duy nhất NGƯỜI SỬ DỤNG nên thực hiện là chấm dứt sử dụng thông tin trên Website.
 
[b]ĐIỀU KHOẢN CUỐI:[/b]
 
Điều khoản sử dụng được điều chỉnh bởi pháp luật Việt Nam
Một điều khoản sử dụng bị vô hiệu theo quyết định của tòa án có thẩm quyền sẽ không ảnh hưởng đến tính hiệu lực của các điều khoản còn lại.
 
Thông báo này và tất cả các điều khoản sử dụng tạo thành toàn bộ thỏa thuận giữa CỬA HÀNG RAU SẠCH FRESH VEGETABLES và NGƯỜI SỬ DỤNG liên quan đến việc sử dụng các thông tin trên website. Chúng tôi có thể điều chỉnh nội dung thông báo này bất cứ lúc nào bằng cách cập nhật lên trang web. NGƯỜI SỬ DỤNG nên thường xuyên truy cập để theo dõi các quy định ràng buộc này khi sử dụng.', 3, 10, 2016, 0, 0, 0 UNION ALL
SELECT 4, 2, N'ĐỊA ĐIỂM PHÂN PHỐI', N'[b]Với mong muốn mang tới cho khách hàng sự thuận tiện nhất và dần thay đổi thói quen đi chợ truyền thống, cũng như đa số khách hàng đều được sử dụng các sản phẩm Nông Sản Sạch chất lượng cao của Chúng tôi[/b]
 
Chúng tôi tiết kiệm tối đa thời gian của Khách hàng. Giá cả luôn được ổn định ( Không phụ thuộc vào yếu tố thời tiết, khách hàng có thể đặt hàng trước cả tuần, tháng...)
Phong cách phục vụ chuyên nghiệp, uy tín. Giao hàng miễn phí tại nhà. Chúng tôi không tính chi phí vận chuyển vào giá thành sản phẩm. (Giá thành sản phẩm được niêm yết trên Chúng tôi tương tự tại cửa hàng)
Khách hàng sẽ được hưởng đúng giá trị của sản phẩm mình mua mà không phải trả bất kỳ 1 chi phí nào khác. 

[b]Kênh bán hàng Online:[/b]
 
Mua hàng thuận tiện dễ dàng. Người dùng chỉ thao tác vài click chuột là có thể gọi hàng từ website. Chức năng quản lý đơn hàng thông minh, Quản lý lượng rau tiêu thụ của gia đình trong tháng... Rất nhiều ưu đãi lớn dành cho khách hàng mua hàng Online.
Mọi giao dịch mua hàng Online đều được Vận chuyển miễn phí tới tận nhà và có hoá đơn bán lẻ từ kho hàng của Chúng tôi.', 4, 10, 2016, 0, 0, 0 UNION ALL
SELECT 5, 2, N'TIN TỨC & KHUYẾN MÃI', N'[b]Tặng thẻ Khách hàng[/b]

Mang đến nhiều tiện ích hơn cho Khách hàng của Chúng tôi. Cũng như nhiều chính sách ưu đãi dành cho Khách hàng có thẻ Chúng tôi

Sau 1 thời gian thử nghiệm phát thẻ Chúng tôi tới một số Khách hàng thân thiết. Từ 20-06-2017 Chúng tôi chính thức triển khai tặng thẻ đối với Khách hàng như sau.

Tặng 01 thẻ Chúng tôi có giá trị 03 năm cho mọi Khách hàng mua sản phẩm của Chúng tôi từ 05 lần trở lên.

Vui lòng giữ lại hoá đơn bán lẻ của Chúng tôi trong 05 lần để yêu cầu nhân viên Chúng tôi tặng thẻ Khách hàng.

[b]Ưu đãi dành cho thẻ Khách hàng:[/b]

Chúng tôi sẽ thường xuyên tổ chức các chương trình ưu đãi dành riêng cho Khách hàng có thẻ Chúng tôi. (Vui lòng thường xuyên theo dõi trên Facebook của Chúng tôi', 5, 10, 2016, 0, 0, 0 UNION ALL
SELECT 6, 2, N'THÔNG BÁO NGHỈ LỄ 2/9', N'[b]NỘI DUNG CỦA THÔNG BÁO NGHỈ LỄ 2/9[/b]', 6, 10, 2020, 0, 0, 0 UNION ALL
SELECT 7, 2, N'ĐIỀU CHỈNH GIÁ BÁN LẺ', N'[b]NỘI DUNG CỦA ĐIỀU CHỈNH GIÁ BÁN LẺ[/b]', 7, 10, 2020, 0, 0, 0 UNION ALL
SELECT 8, 2, N'TẶNG THẺ KHÁCH HÀNG', N'[b]NỘI DUNG CỦA TẶNG THẺ KHÁCH HÀNG[/b]', 8, 10, 2020, 0, 0, 0 UNION ALL
SELECT 9, 2, N'Trang trại trồng rau trong nhà cho năng suất cao', N'[b]Con người đã trải qua hơn 10.000 năm lịch sử nông nghiệp và biết rõ một đợt thiên tai như hạn hán, lũ lụt có thể quét sạch mọi công sức của họ trong năm.[/b] Vì vậy, điều chỉnh nhịp tăng trưởng của cây trồng, tránh bớt thiên tai là điều con người luôn hướng tới.
Để hiện thực hóa ý tưởng trên, nhà sinh học Shigeharu Shimamura đã thiết lập một trang trại trồng rau quy mô lớn trong một nhà máy ở Nhật Bản. Được kiểm soát chặt chẽ bằng cách sử dụng hệ thống đèn LED thiết kế đặc biệt, trang trại trồng rau trong nhà mới mở hồi đầu tháng 7 được cho là có khả năng sản xuất 10.000 cây rau diếp mỗi ngày, theo Daily Mail.
[img]/images_products/trong-rau-nen-300x300.jpg[/img]
Trang trại trồng rau trong nhà có kích cỡ bằng một nửa sân bóng đá, được xây dựng trong một nhà máy trước đây từng sản xuất chất bán dẫn của tập đoàn Sony ở Kashiwa, tỉnh Chiba, Nhật Bản. Trang trại sử dụng 17.500 đèn LED trải rộng trên 18 kệ trồng cao 16 tầng, các đèn chiếu sáng để giả lập ngày và đêm.
Bằng cách giám sát quá trình quang hợp một cách cẩn thận và chi tiết, hệ thống trang trại trồng rau giúp cây rau diếp phát triển nhanh hơn so với rau diếp trồng tại các trang trại ngoài trời với tốc độ 2,5 lần. Nó không chỉ giúp giảm 40% chất thải mà năng suất cũng tăng lên chóng mặt.
Ý tưởng trồng rau sạch, tăng trưởng nhanh, tránh thiên tai đến với Shimamura khi ông làm việc tại tỉnh Miyagi ở miền đông Nhật Bản, nơi này đã bị ảnh hưởng bởi trận động đất mạnh và sóng thần hồi năm 2011. Thảm họa này đã xóa sổ các cây trồng hoa màu, dẫn đến tình trạng thiếu lương thực.
Theo Shimamura, trang trại trồng rau này chỉ sử dụng một lượng nước rất nhỏ, khoảng 1% so với trang trại ngoài trời. Ngoài ra, nó còn giúp điều chỉnh lượng khoáng chất và vitamin trong cây rau.
Các nhà khoa học tin rằng những [b]trang trại trồng rau trong nhà[/b] như thế này sẽ góp phần đắc lực giải quyết tình trạng thiếu hụt lương thực trên thế giới. Kế hoạch sắp tới sẽ là phát triển thêm trang trại tại một số nước trong đó có Hồng Kông.
[i]Theo Thanh niên online[/i]', 9, 10, 2020, 0, 1, 0 UNION ALL
SELECT 10, 2, N'Trồng mít cho năng suất cao – những điều cơ bản', N'[img]/images_products/mit-208x300.jpg[/img]
Mít nghệ cao sản
[b]Như đã giới thiệu, mít là cây ăn trái giúp người dân làm giàu vì vốn đầu tư thấp, tốn ít công chăm sóc và có khả năng chịu hạn. Bà con nông dân nên tham khảo một số điều cơ bản trong cách trồng mít sau đây để bổ sung vào kinh nghiệm trồng mít cho năng suất cao[/b] .
[b]1/ Thiết kế vườn trồng[/b]
- Vườn trồng mít phải thoát nước tốt trong mùa mưa, chống xói mòn để đảm bảo độ phì cho đất, quanh vườn thông thoáng hạn chế sâu bệnh.
- Nếu đất trồng có độ dốc thấp nên đào hố trồng có kích thước 40x40x40cm, trường hợp đất có độ dốc hơi cao đào hố 40x40x60cm (sâu 60cm).
- Tùy theo địa hình của đất cao hay thấp để đào mương thoát nước cho phù hợp nên đào mương phụ rộng và sâu từ 0,3 – 0,4m, mương chính đào rộng và sâu khoảng 0,5 – 0,7m.
[b]2/ Cách trồng mít cho năng suất cao[/b]
- Nên trồng mít vào đầu mùa mưa vì cây con cần nhiều nước trong giai đoạn đầu, nếu trồng cuối mùa mưa thì phải có nước tưới trong mùa khô.
- Đất xấu, cằn cỗi nên trồng mít dày, khoảng 300 cây/hécta,  trong đó trồng cây cách cây 5m, hàng cách hàng 6m. Đất tốt nên trồng mít thưa chỉ khoảng 250 cây/hécta, cây cách cây 5m, hàng cách hàng 8m.
- Trước khi trồng bón lót mỗi gốc 10 – 20kg phân chuồng hoặc 5 – 6kg phân hữu cơ vi sinh với 0,5kg lân, 0,5kg vôi bột và 10 gram Furadan 3G. Đất có độ dốc thấp trồng mặt bầu ngang mặt đất, đất có độ dốc cao trồng mặt bầu thấp hơn mặt đất từ 20 – 30cm. Trước khi trồng cắt đáy bầu, cắt rễ đuôi chuột bị xoắn lại. Sau đó, cắm cọc để cố định cây con, nếu đất khô phải tưới và ủ ẩm cho cây.
- Trong 4 năm đầu cây còn nhỏ có thể trồng xen canh đậu, bắp, rau màu hoặc cỏ lá gừng vừa để chống xói mòn đất vừa tăng thêm thu nhập lấy ngắn nuôi dài.
[b]3/ Chăm sóc[/b]
- Trong năm đầu tiên mỗi cây nên bón 1,5kg vôi bột; 10kg phân hữu cơ; 0,2kg ure; 0,4kg DAP và 0,3kg kali.
- Năm thứ 2 mỗi cây bón 1,5kg vôi bột; 10kg phân hữu cơ; 0,4kg ure; 0,7kg DAP và 0,6kg kali.
- Năm thứ 3 mỗi cây nên bón 1,5kg vôi bột; 10kg phân hữu cơ; 0,6kg ure; 0,9kg DAP và 0,9kg kali. Nếu lắp đặt hệ thống tưới tiết kiệm và bón phân qua đường ống thì từ năm thứ nhất đến năm thứ 3 lượng phân hóa học chia làm 10 lần bón/năm.
- Từ năm thứ 4 trở đi cây bắt đầu cho trái nhiều, sau khi thu hoạch bón mỗi gốc 20 – 30kg phân chuồng ủ hoai, 1kg vôi bột. Còn phân hóa học sau khi thu hoạch chia làm 3 lần để bón, mỗi lần cách nhau 10 ngày. Số lượng phân bón cho mỗi gốc khoảng 0,3kg ure; 0,2kg DAP; 0,15kg kali/lần. Đến thời điểm cây ra hoa bón 3 lần phân hóa học, mỗi lần cách nhau 10 ngày số lượng 0,15kg DAP; 0,1kg kali/lần. Lúc cây đậu trái và nuôi trái bón 0,8kg ure và 0,35kg kali/cây.
- Khi làm cỏ cho cây mít chú ý, rễ mít thường mọc nổi không cuốc sâu quanh gốc làm sẽ đứt rễ. Đặc biệt, trong giai đoạn đang cho trái nếu làm cỏ để đứt rễ dinh dưỡng bị xáo trộn, trái sẽ nhỏ, chất lượng giảm và đôi khi múi còn bị sượng. Nếu có điều kiện nên lắp đặt hệ thống tưới tiết kiệm và bón phân qua đường ống sẽ giảm được nhiều công tưới, bón phân hóa học, trong khi năng suất chất lượng trái tăng cao.
[b]4/ Tỉa cành tạo tán[/b]
- Chỉ tỉa cành tạo tán khi cây mít đạt chiều cao khoảng 1m trở lên, khi cây chưa cho trái tỉa cành 2-3 lần/năm. Cây đã cho trái tỉa cành 1 năm/lần vào thời điểm thu hoạch trái xong.
- Cắt bỏ các cành gần sát mặt đất, cành tược, cành nhỏ mọc không đúng hướng, cành sâu bệnh. Giữ lại cành cấp 1 cách gốc khoảng 40cm trở lên, chọn các cành mọc theo các hướng khác nhau, cành trên cách cành dưới khoảng 40 – 50cm, tạo thành tầng không quá 5 cành cấp 1. Tỉa bỏ bớt cành cấp 2, cấp 3… cho cây thoáng nhằm chống sâu bệnh và tăng năng suất.
[b]5/ Bảo quản và thu hoạch[/b]
- Cây mít cho trái rải vụ quanh năm, song vụ chính ở Đồng Nai vào tháng 6, 7. Thời gian từ lúc ra hoa đến lúc trái già khoảng 5 tháng, do đó có thể căn cứ vào màu sắc trái để thu hoạch. Trái mít già, các gai nở căng, chuyển từ màu xanh sang màu xanh vàng hoặc nâu nhạt, mủ lỏng và trong, vỗ kêu bồm bộp, để vận chuyển đi xa nên thu hoạch lúc trái già.
Hiện ở Đồng Nai có nhiều giống mít cho năng suất cao như: mít Viên Linh, mít Thái, mít nghệ, mít tố nữ… Song, nông dân nên trồng giống mít nghệ trong nước được tuyển chọn qua các cuộc thi trái ngon, giống tốt hoặc những giống mít trong nước có phẩm chất ngon được nhiều người ưa chuộng, đồng thời thích hợp để chế biến, sấy khô đóng gói xuất khẩu.
[i]Bộ NN & PTNN Việt Nam[/i]', 10, 10, 2020, 0, 1, 0
COMMIT;
RAISERROR (N'[dbo].[tin_tuc]: INSERT BATCH: .....DONE!', 10, 1) WITH NOWAIT;
GO

SET IDENTITY_INSERT [dbo].[tin_tuc] OFF;

SET IDENTITY_INSERT [dbo].[don_hang_chi_tiet] ON;

BEGIN TRANSACTION;
INSERT INTO [dbo].[don_hang_chi_tiet]([id_dhct], [id_dh], [id_sp], [don_gia], [so_luong], [id_dvt])
SELECT 1, 1, 5, 30000, 1, 1 UNION ALL
SELECT 2, 1, 2, 85000, 2, 1 UNION ALL
SELECT 3, 1, 8, 30000, 1, 1 UNION ALL
SELECT 4, 1, 11, 30000, 1, 1 UNION ALL
SELECT 5, 2, 5, 30000, 1, 1 UNION ALL
SELECT 6, 2, 2, 85000, 2, 1 UNION ALL
SELECT 7, 2, 8, 30000, 1, 1 UNION ALL
SELECT 8, 2, 11, 30000, 1, 1
COMMIT;
RAISERROR (N'[dbo].[don_hang_chi_tiet]: INSERT BATCH: .....DONE!', 10, 1) WITH NOWAIT;
GO

SET IDENTITY_INSERT [dbo].[don_hang_chi_tiet] OFF;

BEGIN TRANSACTION;
INSERT INTO [dbo].[gio_hang]([id_tv], [id_sp], [don_gia], [so_luong], [id_dvt])
SELECT 3, 5, 30000, 1, 1 UNION ALL
SELECT 3, 2, 85000, 2, 1 UNION ALL
SELECT 3, 8, 30000, 1, 1 UNION ALL
SELECT 3, 11, 30000, 1, 1 UNION ALL
SELECT 4, 5, 30000, 1, 1 UNION ALL
SELECT 4, 2, 85000, 2, 1 UNION ALL
SELECT 4, 8, 30000, 1, 1 UNION ALL
SELECT 4, 11, 30000, 1, 1
COMMIT;
RAISERROR (N'[dbo].[gio_hang]: INSERT BATCH: .....DONE!', 10, 1) WITH NOWAIT;
GO

SET IDENTITY_INSERT [dbo].[hoi_dap] ON;

BEGIN TRANSACTION;
INSERT INTO [dbo].[hoi_dap]([id_hd], [id_tv], [tieu_de], [noi_dung], [ngay_hd], [thang_hd], [nam_hd], [close_hd], [chia_se])
SELECT 1, 3, N'Mua hàng ở đâu', N'Chúng tôi có thể mua hàng của cửa hàng ở đâu?', 1, 10, 2020, 1, 1 UNION ALL
SELECT 2, 4, N'Làm sao biết được cửa hàng bán rau sạch', N'Làm sao biết được cửa hàng bán rau sạch?', 2, 10, 2020, 1, 1 UNION ALL
SELECT 3, 5, N'Tôi có thể truy cứu nguồn gốc sản phẩm?', N'Tôi có thể truy cứu nguồn gốc sản phẩm?', 3, 10, 2020, 1, 1 UNION ALL
SELECT 4, 6, N'Tôi muốn mua hàng tận nơi?', N'Tôi muốn mua hàng tận nơi?', 4, 10, 2020, 1, 1 UNION ALL
SELECT 5, 3, N'Tôi muốn thăm quan nơi sản xuất của cửa hàng?', N'Tôi muốn thăm quan nơi sản xuất của cửa hàng?', 5, 10, 2020, 0, 1 UNION ALL
SELECT 6, 4, N'Tôi muốn làm đại lý của cửa hàng?', N'Tôi muốn làm đại lý của cửa hàng?', 6, 10, 2020, 0, 1
COMMIT;
RAISERROR (N'[dbo].[hoi_dap]: INSERT BATCH: .....DONE!', 10, 1) WITH NOWAIT;
GO

SET IDENTITY_INSERT [dbo].[hoi_dap] OFF;

GO

SET IDENTITY_INSERT [dbo].[hoi_dap_tra_loi] ON;

BEGIN TRANSACTION;
INSERT INTO [dbo].[hoi_dap_tra_loi]([id_traloi], [id_hd], [id_tv], [noi_dung], [ngay_traloi], [thang_traloi], [nam_traloi])
SELECT 1, 1, 2, N'Khách hàng có thể đến địa chỉ cửa hàng hoặc đặt hàng trực tuyến trên website này.', 1, 11, 2020 UNION ALL
SELECT 2, 2, 2, N'Để trả lời câu hỏi trên thì trước hết bạn cần hiểu về quy trình hoạt động của chúng tôi. Chúng tôi là doanh nghiệp sản xuất và bán lẻ đến tận tay người dùng, chúng tôi không đặt hàng hoặc thu mua sản phẩm từ một đối tác thứ 2 hoặc thứ 3 sau đó phân phối lại cho người dùng.

Các sản phẩm của chúng tôiđều được canh tác tại Nông trại được sản xuất trên các hệ thống tiêu chuẩn quản lý thực hành nông nghiệp tốt nhất hiện nay (VietGAP) Với hệ thống nhà kính tiên tiến và hệ thống kiểm soát khí hậu, hệ thống xử lý nước và phun tưới, hệ thống xử lý sau thu hoạch...Kết hợp với sự tư vấn và hợp tác chặt chẽ của các Kỹ Sư, Tiến Sỹ có uy tín hàng đầu về Nông Nghiệp.

Khi sử dụng các Sản Phẩm của chúng tôikhách hàng có thể tra cứu nguồn gốc sản phẩm dễ dàng qua những thông tin thể hiện trên bao bì Sản Phẩm cũng như từ website của chúng tôi. Các thông tin này bao gồm hạn sử dụng, thành phần dinh dưỡng của sản phẩm, khu vực sản xuất, quy trình canh tác, ngày tháng thu hái, hạn sử dụng...

Các sản phẩm của chúng tôi đều được cấp chứng nhận tiêu chuẩn VietGAP do Tổng Cục Tiêu Chuẩn Đo Lường Chất Lượng Bộ Khoa Học (QUACERT) chứng nhận.

Bạn có thể tìm hiểu thêm tại website này hoặc Google với từ khoá "Tiêu Chuẩn VietGAP là gì?"', 2, 11, 2020 UNION ALL
SELECT 3, 3, 2, N'Vâng. Bạn hoàn toàn có thể truy cứu nguồn gốc các sản phẩm của chúng tôi trên website này.', 3, 11, 2020 UNION ALL
SELECT 4, 4, 2, N'Năm 2016 chúng tôi chỉ tập trung vào phân phối các sản phẩm Online.

Nên chúng tôi không phân phối theo hình thức cửa hàng bán và giới thiệu sản phẩm.

Tuy nhiên bạn vẫn có thể đến địa chỉ của công ty để mua hàng.', 4, 11, 2020 UNION ALL
SELECT 5, 5, 2, N'Bạn hoàn toàn có quyền thăm quan nơi sản xuất của nông trại chúng tôi tại Bình Dương.

Tuy nhiên bạn cần có sự cho phép của đại diện nông trại tại Bình Dương.', 5, 11, 2020 UNION ALL
SELECT 6, 6, 2, N'Để trở thành đại lý phân phối các sản phẩm của cửa hạn bạn vui lòng gọi về cho chúng tôi.', 6, 11, 2020
COMMIT;
RAISERROR (N'[dbo].[hoi_dap_tra_loi]: INSERT BATCH: .....DONE!', 10, 1) WITH NOWAIT;
GO

SET IDENTITY_INSERT [dbo].[hoi_dap_tra_loi] OFF;

