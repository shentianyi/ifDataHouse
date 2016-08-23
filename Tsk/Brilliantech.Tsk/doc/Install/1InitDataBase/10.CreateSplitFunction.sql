CREATE   FUNCTION [dbo].[Split]  
(  
@c VARCHAR(MAX) ,  
@split VARCHAR(50)  
)  
RETURNS @t TABLE ( col VARCHAR(50) )  
AS  
BEGIN  
    WHILE ( CHARINDEX(@split, @c) <> 0 )  
        BEGIN  
            INSERT  @t( col )  
            VALUES  ( SUBSTRING(@c, 1, CHARINDEX(@split, @c) - 1) )  
            SET @c = STUFF(@c, 1, CHARINDEX(@split, @c), '')  
        END  
    INSERT  @t( col ) VALUES  ( @c )  
    RETURN  
END  