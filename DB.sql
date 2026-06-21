CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(150) CHARACTER SET utf8mb4 NOT NULL,
    `ProductVersion` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK___EFMigrationsHistory` PRIMARY KEY (`MigrationId`)
) CHARACTER SET=utf8mb4;

START TRANSACTION;

ALTER DATABASE CHARACTER SET utf8mb4;

CREATE TABLE `Domains` (
    `Id` char(36) COLLATE ascii_general_ci NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK_Domains` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `Skills` (
    `Id` char(36) COLLATE ascii_general_ci NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK_Skills` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `Users` (
    `Id` char(36) COLLATE ascii_general_ci NOT NULL,
    `Email` longtext CHARACTER SET utf8mb4 NOT NULL,
    `PasswordHash` longtext CHARACTER SET utf8mb4 NOT NULL,
    `FullName` longtext CHARACTER SET utf8mb4 NOT NULL,
    `Role` longtext CHARACTER SET utf8mb4 NOT NULL,
    `Status` longtext CHARACTER SET utf8mb4 NOT NULL,
    `AvatarUrl` longtext CHARACTER SET utf8mb4 NULL,
    `CreatedAt` datetime(6) NOT NULL,
    CONSTRAINT `PK_Users` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `Specializations` (
    `Id` char(36) COLLATE ascii_general_ci NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NOT NULL,
    `DomainId` char(36) COLLATE ascii_general_ci NOT NULL,
    CONSTRAINT `PK_Specializations` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Specializations_Domains_DomainId` FOREIGN KEY (`DomainId`) REFERENCES `Domains` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `ExpertProfiles` (
    `UserId` char(36) COLLATE ascii_general_ci NOT NULL,
    `JobTitle` longtext CHARACTER SET utf8mb4 NOT NULL,
    `Major` longtext CHARACTER SET utf8mb4 NOT NULL,
    `Certifications` longtext CHARACTER SET utf8mb4 NULL,
    `Bio` longtext CHARACTER SET utf8mb4 NOT NULL,
    `PortfolioUrls` longtext CHARACTER SET utf8mb4 NULL,
    `ReputationCredit` decimal(18,2) NOT NULL,
    `Location` longtext CHARACTER SET utf8mb4 NULL,
    `SuccessRate` double NOT NULL,
    CONSTRAINT `PK_ExpertProfiles` PRIMARY KEY (`UserId`),
    CONSTRAINT `FK_ExpertProfiles_Users_UserId` FOREIGN KEY (`UserId`) REFERENCES `Users` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `Wallets` (
    `UserId` char(36) COLLATE ascii_general_ci NOT NULL,
    `Balance` decimal(18,2) NOT NULL,
    CONSTRAINT `PK_Wallets` PRIMARY KEY (`UserId`),
    CONSTRAINT `FK_Wallets_Users_UserId` FOREIGN KEY (`UserId`) REFERENCES `Users` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `JobPosts` (
    `Id` char(36) COLLATE ascii_general_ci NOT NULL,
    `ClientId` char(36) COLLATE ascii_general_ci NOT NULL,
    `Title` longtext CHARACTER SET utf8mb4 NOT NULL,
    `Description` longtext CHARACTER SET utf8mb4 NOT NULL,
    `Budget` decimal(18,2) NOT NULL,
    `Deadline` int NOT NULL,
    `Status` longtext CHARACTER SET utf8mb4 NOT NULL,
    `CreatedAt` datetime(6) NOT NULL,
    `DomainId` char(36) COLLATE ascii_general_ci NULL,
    `SpecializationId` char(36) COLLATE ascii_general_ci NULL,
    `DurationUnit` longtext CHARACTER SET utf8mb4 NULL,
    `DurationValue` int NOT NULL,
    CONSTRAINT `PK_JobPosts` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_JobPosts_Domains_DomainId` FOREIGN KEY (`DomainId`) REFERENCES `Domains` (`Id`),
    CONSTRAINT `FK_JobPosts_Specializations_SpecializationId` FOREIGN KEY (`SpecializationId`) REFERENCES `Specializations` (`Id`),
    CONSTRAINT `FK_JobPosts_Users_ClientId` FOREIGN KEY (`ClientId`) REFERENCES `Users` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `DomainExpertProfiles` (
    `DomainId` char(36) COLLATE ascii_general_ci NOT NULL,
    `ExpertProfilesUserId` char(36) COLLATE ascii_general_ci NOT NULL,
    CONSTRAINT `PK_DomainExpertProfiles` PRIMARY KEY (`DomainId`, `ExpertProfilesUserId`),
    CONSTRAINT `FK_DomainExpertProfiles_Domains_DomainId` FOREIGN KEY (`DomainId`) REFERENCES `Domains` (`Id`),
    CONSTRAINT `FK_DomainExpertProfiles_ExpertProfiles_ExpertProfilesUserId` FOREIGN KEY (`ExpertProfilesUserId`) REFERENCES `ExpertProfiles` (`UserId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `ExpertProfileSkill` (
    `ExpertProfilesUserId` char(36) COLLATE ascii_general_ci NOT NULL,
    `SkillsId` char(36) COLLATE ascii_general_ci NOT NULL,
    CONSTRAINT `PK_ExpertProfileSkill` PRIMARY KEY (`ExpertProfilesUserId`, `SkillsId`),
    CONSTRAINT `FK_ExpertProfileSkill_ExpertProfiles_ExpertProfilesUserId` FOREIGN KEY (`ExpertProfilesUserId`) REFERENCES `ExpertProfiles` (`UserId`),
    CONSTRAINT `FK_ExpertProfileSkill_Skills_SkillsId` FOREIGN KEY (`SkillsId`) REFERENCES `Skills` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `Conversations` (
    `Id` char(36) COLLATE ascii_general_ci NOT NULL,
    `OriginJobPostId` char(36) COLLATE ascii_general_ci NULL,
    `ClientId` char(36) COLLATE ascii_general_ci NOT NULL,
    `ExpertId` char(36) COLLATE ascii_general_ci NOT NULL,
    `CreatedAt` datetime(6) NOT NULL,
    CONSTRAINT `PK_Conversations` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Conversations_JobPosts_OriginJobPostId` FOREIGN KEY (`OriginJobPostId`) REFERENCES `JobPosts` (`Id`),
    CONSTRAINT `FK_Conversations_Users_ClientId` FOREIGN KEY (`ClientId`) REFERENCES `Users` (`Id`),
    CONSTRAINT `FK_Conversations_Users_ExpertId` FOREIGN KEY (`ExpertId`) REFERENCES `Users` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `JobPostSkill` (
    `JobPostsId` char(36) COLLATE ascii_general_ci NOT NULL,
    `SkillsId` char(36) COLLATE ascii_general_ci NOT NULL,
    CONSTRAINT `PK_JobPostSkill` PRIMARY KEY (`JobPostsId`, `SkillsId`),
    CONSTRAINT `FK_JobPostSkill_JobPosts_JobPostsId` FOREIGN KEY (`JobPostsId`) REFERENCES `JobPosts` (`Id`),
    CONSTRAINT `FK_JobPostSkill_Skills_SkillsId` FOREIGN KEY (`SkillsId`) REFERENCES `Skills` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `JobRequirements` (
    `Id` char(36) COLLATE ascii_general_ci NOT NULL,
    `JobPostId` char(36) COLLATE ascii_general_ci NOT NULL,
    `UseCaseName` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_JobRequirements` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_JobRequirements_JobPosts_JobPostId` FOREIGN KEY (`JobPostId`) REFERENCES `JobPosts` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `Proposals` (
    `Id` char(36) COLLATE ascii_general_ci NOT NULL,
    `JobPostId` char(36) COLLATE ascii_general_ci NOT NULL,
    `ExpertId` char(36) COLLATE ascii_general_ci NOT NULL,
    `BidAmount` decimal(18,2) NOT NULL,
    `EstimatedDuration` int NOT NULL,
    `Title` longtext CHARACTER SET utf8mb4 NOT NULL,
    `Introduction` longtext CHARACTER SET utf8mb4 NOT NULL,
    `Technical` longtext CHARACTER SET utf8mb4 NOT NULL,
    `Implementation` longtext CHARACTER SET utf8mb4 NOT NULL,
    `Dependencies` longtext CHARACTER SET utf8mb4 NOT NULL,
    `Portfolio` longtext CHARACTER SET utf8mb4 NULL,
    `Status` longtext CHARACTER SET utf8mb4 NOT NULL,
    `CreatedAt` datetime(6) NOT NULL,
    CONSTRAINT `PK_Proposals` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Proposals_JobPosts_JobPostId` FOREIGN KEY (`JobPostId`) REFERENCES `JobPosts` (`Id`),
    CONSTRAINT `FK_Proposals_Users_ExpertId` FOREIGN KEY (`ExpertId`) REFERENCES `Users` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `Messages` (
    `Id` char(36) COLLATE ascii_general_ci NOT NULL,
    `ConversationId` char(36) COLLATE ascii_general_ci NOT NULL,
    `SenderId` char(36) COLLATE ascii_general_ci NOT NULL,
    `Content` longtext CHARACTER SET utf8mb4 NOT NULL,
    `IsRead` tinyint(1) NOT NULL,
    `CreatedAt` datetime(6) NOT NULL,
    CONSTRAINT `PK_Messages` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Messages_Conversations_ConversationId` FOREIGN KEY (`ConversationId`) REFERENCES `Conversations` (`Id`),
    CONSTRAINT `FK_Messages_Users_SenderId` FOREIGN KEY (`SenderId`) REFERENCES `Users` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `Projects` (
    `Id` char(36) COLLATE ascii_general_ci NOT NULL,
    `JobPostId` char(36) COLLATE ascii_general_ci NULL,
    `ClientId` char(36) COLLATE ascii_general_ci NOT NULL,
    `ExpertId` char(36) COLLATE ascii_general_ci NOT NULL,
    `EscrowBalance` decimal(18,2) NOT NULL,
    `Status` longtext CHARACTER SET utf8mb4 NOT NULL,
    `StartDate` datetime(6) NOT NULL,
    `EndDate` datetime(6) NULL,
    `ProjectLink` longtext CHARACTER SET utf8mb4 NULL,
    `ConversationId` char(36) COLLATE ascii_general_ci NULL,
    CONSTRAINT `PK_Projects` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Projects_Conversations_ConversationId` FOREIGN KEY (`ConversationId`) REFERENCES `Conversations` (`Id`),
    CONSTRAINT `FK_Projects_JobPosts_JobPostId` FOREIGN KEY (`JobPostId`) REFERENCES `JobPosts` (`Id`),
    CONSTRAINT `FK_Projects_Users_ClientId` FOREIGN KEY (`ClientId`) REFERENCES `Users` (`Id`),
    CONSTRAINT `FK_Projects_Users_ExpertId` FOREIGN KEY (`ExpertId`) REFERENCES `Users` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `ProjectSkill` (
    `ProjectsId` char(36) COLLATE ascii_general_ci NOT NULL,
    `SkillsId` char(36) COLLATE ascii_general_ci NOT NULL,
    CONSTRAINT `PK_ProjectSkill` PRIMARY KEY (`ProjectsId`, `SkillsId`),
    CONSTRAINT `FK_ProjectSkill_Projects_ProjectsId` FOREIGN KEY (`ProjectsId`) REFERENCES `Projects` (`Id`),
    CONSTRAINT `FK_ProjectSkill_Skills_SkillsId` FOREIGN KEY (`SkillsId`) REFERENCES `Skills` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `Reviews` (
    `Id` char(36) COLLATE ascii_general_ci NOT NULL,
    `ProjectId` char(36) COLLATE ascii_general_ci NOT NULL,
    `CreatedById` char(36) COLLATE ascii_general_ci NOT NULL,
    `TargetUserId` char(36) COLLATE ascii_general_ci NOT NULL,
    `Rating` int NOT NULL,
    `Comment` longtext CHARACTER SET utf8mb4 NULL,
    `CreatedAt` datetime(6) NOT NULL,
    CONSTRAINT `PK_Reviews` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Reviews_Projects_ProjectId` FOREIGN KEY (`ProjectId`) REFERENCES `Projects` (`Id`),
    CONSTRAINT `FK_Reviews_Users_CreatedById` FOREIGN KEY (`CreatedById`) REFERENCES `Users` (`Id`),
    CONSTRAINT `FK_Reviews_Users_TargetUserId` FOREIGN KEY (`TargetUserId`) REFERENCES `Users` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `Tasks` (
    `Id` char(36) COLLATE ascii_general_ci NOT NULL,
    `ProjectId` char(36) COLLATE ascii_general_ci NOT NULL,
    `Title` longtext CHARACTER SET utf8mb4 NOT NULL,
    `Status` longtext CHARACTER SET utf8mb4 NOT NULL,
    `UpdatedAt` datetime(6) NOT NULL,
    CONSTRAINT `PK_Tasks` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Tasks_Projects_ProjectId` FOREIGN KEY (`ProjectId`) REFERENCES `Projects` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `TransactionLogs` (
    `Id` char(36) COLLATE ascii_general_ci NOT NULL,
    `ProjectId` char(36) COLLATE ascii_general_ci NULL,
    `SourceWalletId` char(36) COLLATE ascii_general_ci NULL,
    `DestinationWalletId` char(36) COLLATE ascii_general_ci NULL,
    `Amount` decimal(18,2) NOT NULL,
    `Type` longtext CHARACTER SET utf8mb4 NOT NULL,
    `CreatedAt` datetime(6) NOT NULL,
    CONSTRAINT `PK_TransactionLogs` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_TransactionLogs_Projects_ProjectId` FOREIGN KEY (`ProjectId`) REFERENCES `Projects` (`Id`),
    CONSTRAINT `FK_TransactionLogs_Wallets_DestinationWalletId` FOREIGN KEY (`DestinationWalletId`) REFERENCES `Wallets` (`UserId`),
    CONSTRAINT `FK_TransactionLogs_Wallets_SourceWalletId` FOREIGN KEY (`SourceWalletId`) REFERENCES `Wallets` (`UserId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `MiniTasks` (
    `Id` char(36) COLLATE ascii_general_ci NOT NULL,
    `TaskId` char(36) COLLATE ascii_general_ci NOT NULL,
    `Title` longtext CHARACTER SET utf8mb4 NOT NULL,
    `IsCompleted` tinyint(1) NOT NULL,
    `FeedbackContent` longtext CHARACTER SET utf8mb4 NULL,
    `FeedbackSenderId` char(36) COLLATE ascii_general_ci NULL,
    `CreatedAt` datetime(6) NOT NULL,
    CONSTRAINT `PK_MiniTasks` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_MiniTasks_Tasks_TaskId` FOREIGN KEY (`TaskId`) REFERENCES `Tasks` (`Id`),
    CONSTRAINT `FK_MiniTasks_Users_FeedbackSenderId` FOREIGN KEY (`FeedbackSenderId`) REFERENCES `Users` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE INDEX `IX_Conversations_ClientId` ON `Conversations` (`ClientId`);

CREATE INDEX `IX_Conversations_ExpertId` ON `Conversations` (`ExpertId`);

CREATE INDEX `IX_Conversations_OriginJobPostId` ON `Conversations` (`OriginJobPostId`);

CREATE INDEX `IX_DomainExpertProfiles_ExpertProfilesUserId` ON `DomainExpertProfiles` (`ExpertProfilesUserId`);

CREATE INDEX `IX_ExpertProfileSkill_SkillsId` ON `ExpertProfileSkill` (`SkillsId`);

CREATE INDEX `IX_JobPosts_ClientId` ON `JobPosts` (`ClientId`);

CREATE INDEX `IX_JobPosts_DomainId` ON `JobPosts` (`DomainId`);

CREATE INDEX `IX_JobPosts_SpecializationId` ON `JobPosts` (`SpecializationId`);

CREATE INDEX `IX_JobPostSkill_SkillsId` ON `JobPostSkill` (`SkillsId`);

CREATE INDEX `IX_JobRequirements_JobPostId` ON `JobRequirements` (`JobPostId`);

CREATE INDEX `IX_Messages_ConversationId` ON `Messages` (`ConversationId`);

CREATE INDEX `IX_Messages_SenderId` ON `Messages` (`SenderId`);

CREATE INDEX `IX_MiniTasks_FeedbackSenderId` ON `MiniTasks` (`FeedbackSenderId`);

CREATE INDEX `IX_MiniTasks_TaskId` ON `MiniTasks` (`TaskId`);

CREATE INDEX `IX_Projects_ClientId` ON `Projects` (`ClientId`);

CREATE INDEX `IX_Projects_ConversationId` ON `Projects` (`ConversationId`);

CREATE INDEX `IX_Projects_ExpertId` ON `Projects` (`ExpertId`);

CREATE INDEX `IX_Projects_JobPostId` ON `Projects` (`JobPostId`);

CREATE INDEX `IX_ProjectSkill_SkillsId` ON `ProjectSkill` (`SkillsId`);

CREATE INDEX `IX_Proposals_ExpertId` ON `Proposals` (`ExpertId`);

CREATE UNIQUE INDEX `IX_Proposals_JobPostId_ExpertId` ON `Proposals` (`JobPostId`, `ExpertId`);

CREATE INDEX `IX_Reviews_CreatedById` ON `Reviews` (`CreatedById`);

CREATE INDEX `IX_Reviews_ProjectId` ON `Reviews` (`ProjectId`);

CREATE INDEX `IX_Reviews_TargetUserId` ON `Reviews` (`TargetUserId`);

CREATE INDEX `IX_Specializations_DomainId` ON `Specializations` (`DomainId`);

CREATE INDEX `IX_Tasks_ProjectId` ON `Tasks` (`ProjectId`);

CREATE INDEX `IX_TransactionLogs_DestinationWalletId` ON `TransactionLogs` (`DestinationWalletId`);

CREATE INDEX `IX_TransactionLogs_ProjectId` ON `TransactionLogs` (`ProjectId`);

CREATE INDEX `IX_TransactionLogs_SourceWalletId` ON `TransactionLogs` (`SourceWalletId`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20260621053820_InitialMySql', '8.0.11');

COMMIT;

