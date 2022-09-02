DROP TRIGGER IF EXISTS `lanflex`.`filesdetails_AFTER_INSERT`;

DELIMITER $$
USE `lanflex`$$
CREATE DEFINER = CURRENT_USER TRIGGER `lanflex`.`filesdetails_AFTER_INSERT` AFTER INSERT ON `filesdetails` FOR EACH ROW
BEGIN
	INSERT INTO history(fileId, UploadedTime)
	VALUES(New.FileId, NEW.CreatedAt);
END$$
DELIMITER ;
