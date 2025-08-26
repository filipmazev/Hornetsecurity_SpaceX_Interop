/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

IF '$(BuildConfiguration)' = 'Debug'
BEGIN
    BEGIN TRANSACTION;
    PRINT 'Running post-deployment script for Debug configuration.';

    --#region Seed Data

    -- TODO: Add Users Seed Data here

    --#endregion

    COMMIT TRANSACTION;
END