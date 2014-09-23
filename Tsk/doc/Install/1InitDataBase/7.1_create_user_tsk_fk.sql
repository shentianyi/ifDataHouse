USE [Leoni_Tsk_JN]
GO

ALTER TABLE [dbo].[UserTsk]  WITH CHECK ADD  CONSTRAINT [FK_UserTsk_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO

ALTER TABLE [dbo].[UserTsk] CHECK CONSTRAINT [FK_UserTsk_User]
GO


