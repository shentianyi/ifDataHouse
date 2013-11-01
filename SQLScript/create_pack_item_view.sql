USE [LeoniPacking_prod]
GO

/****** Object:  View [dbo].[PackItemView]    Script Date: 10/29/2013 10:14:09 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[PackItemView]
AS
SELECT     dbo.PackageItem.packageID, dbo.ProdLine.projectID, dbo.WorkStation.wrkStnID, dbo.SinglePackage.partNr, dbo.PackageItem.packagingTime
FROM         dbo.PackageItem INNER JOIN
                      dbo.SinglePackage ON dbo.PackageItem.packageID = dbo.SinglePackage.packageID INNER JOIN
                      dbo.WorkStation ON dbo.SinglePackage.wrkstnID = dbo.WorkStation.wrkStnID INNER JOIN
                      dbo.ProdLine ON dbo.WorkStation.prodLineID = dbo.ProdLine.prodLineID

GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[41] 4[12] 2[18] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "PackageItem"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 206
               Right = 197
            End
            DisplayFlags = 280
            TopColumn = 1
         End
         Begin Table = "ProdLine"
            Begin Extent = 
               Top = 6
               Left = 235
               Bottom = 110
               Right = 392
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "SinglePackage"
            Begin Extent = 
               Top = 6
               Left = 430
               Bottom = 201
               Right = 575
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "WorkStation"
            Begin Extent = 
               Top = 114
               Left = 235
               Bottom = 233
               Right = 384
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 2085
         Width = 1500
         Width = 2040
         Width = 2835
         Width = 1965
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'PackItemView'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'PackItemView'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'PackItemView'
GO


