using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudyTaskManager.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class testMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LogAction",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogAction", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SystemRole",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CanViewPeoplesGroups = table.Column<bool>(type: "boolean", nullable: false),
                    CanChangeSystemRoles = table.Column<bool>(type: "boolean", nullable: false),
                    CanBlockUsers = table.Column<bool>(type: "boolean", nullable: false),
                    CanDeleteChats = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemRole", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    PhoneNumber = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: true),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    SystemRoleId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_SystemRole_SystemRoleId",
                        column: x => x.SystemRoleId,
                        principalTable: "SystemRole",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BlockedUserInfo",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    PrevRoleId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlockedUserInfo", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_BlockedUserInfo_SystemRole_PrevRoleId",
                        column: x => x.PrevRoleId,
                        principalTable: "SystemRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BlockedUserInfo_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonalChat",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    User1Id = table.Column<Guid>(type: "uuid", nullable: false),
                    User2Id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalChat", x => x.Id);
                    table.UniqueConstraint("AK_PersonalChat_User1Id_User2Id", x => new { x.User1Id, x.User2Id });
                    table.ForeignKey(
                        name: "FK_PersonalChat_User_User1Id",
                        column: x => x.User1Id,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PersonalChat_User_User2Id",
                        column: x => x.User2Id,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PersonalMessage",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SenderId = table.Column<Guid>(type: "uuid", nullable: false),
                    PersonalChatId = table.Column<Guid>(type: "uuid", nullable: false),
                    Content = table.Column<string>(type: "character varying(5000)", maxLength: 5000, nullable: false),
                    Is_Read_By_Other_User = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalMessage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonalMessage_PersonalChat_PersonalChatId",
                        column: x => x.PersonalChatId,
                        principalTable: "PersonalChat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonalMessage_User_SenderId",
                        column: x => x.SenderId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Group",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(5000)", maxLength: 5000, nullable: true),
                    DefaultRoleId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Group", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GroupChat",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    GroupId = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    IsPublic = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupChat", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupChat_Group_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Group",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GroupInvite",
                columns: table => new
                {
                    SenderId = table.Column<Guid>(type: "uuid", nullable: false),
                    ReceiverId = table.Column<Guid>(type: "uuid", nullable: false),
                    GroupId = table.Column<Guid>(type: "uuid", nullable: false),
                    InvitationAccepted = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupInvite", x => new { x.SenderId, x.ReceiverId, x.GroupId });
                    table.ForeignKey(
                        name: "FK_GroupInvite_Group_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Group",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupInvite_User_ReceiverId",
                        column: x => x.ReceiverId,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GroupInvite_User_SenderId",
                        column: x => x.SenderId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "GroupRole",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    GroupId = table.Column<Guid>(type: "uuid", nullable: true),
                    RoleName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CanCreateTasks = table.Column<bool>(type: "boolean", nullable: false),
                    CanManageRoles = table.Column<bool>(type: "boolean", nullable: false),
                    CanCreateTaskUpdates = table.Column<bool>(type: "boolean", nullable: false),
                    CanChangeTaskUpdates = table.Column<bool>(type: "boolean", nullable: false),
                    CanInviteUsers = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupRole", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupRole_Group_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Group",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "GroupTaskStatus",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    GroupId = table.Column<Guid>(type: "uuid", nullable: true),
                    CanBeUpdated = table.Column<bool>(type: "boolean", nullable: false),
                    Description = table.Column<string>(type: "character varying(5000)", maxLength: 5000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupTaskStatus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupTaskStatus_Group_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Group",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Log",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    LogActionId = table.Column<Guid>(type: "uuid", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    GroupId = table.Column<Guid>(type: "uuid", nullable: true),
                    InitiatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    SubjectId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Log", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Log_Group_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Group",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Log_LogAction_LogActionId",
                        column: x => x.LogActionId,
                        principalTable: "LogAction",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Log_User_InitiatorId",
                        column: x => x.InitiatorId,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Log_User_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "GroupChatMessage",
                columns: table => new
                {
                    GroupChatId = table.Column<Guid>(type: "uuid", nullable: false),
                    Ordinal = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    SenderId = table.Column<Guid>(type: "uuid", nullable: false),
                    Context = table.Column<string>(type: "character varying(5000)", maxLength: 5000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupChatMessage", x => new { x.GroupChatId, x.Ordinal });
                    table.ForeignKey(
                        name: "FK_GroupChatMessage_GroupChat_GroupChatId",
                        column: x => x.GroupChatId,
                        principalTable: "GroupChat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupChatMessage_User_SenderId",
                        column: x => x.SenderId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GroupChatParticipant",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    GroupChatId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupChatParticipant", x => new { x.GroupChatId, x.UserId });
                    table.ForeignKey(
                        name: "FK_GroupChatParticipant_GroupChat_GroupChatId",
                        column: x => x.GroupChatId,
                        principalTable: "GroupChat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupChatParticipant_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GroupGroupRole",
                columns: table => new
                {
                    GroupRolesId = table.Column<Guid>(type: "uuid", nullable: false),
                    GroupsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupGroupRole", x => new { x.GroupRolesId, x.GroupsId });
                    table.ForeignKey(
                        name: "FK_GroupGroupRole_GroupRole_GroupRolesId",
                        column: x => x.GroupRolesId,
                        principalTable: "GroupRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupGroupRole_Group_GroupsId",
                        column: x => x.GroupsId,
                        principalTable: "Group",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserInGroup",
                columns: table => new
                {
                    GroupId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInGroup", x => new { x.UserId, x.GroupId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserInGroup_GroupRole_RoleId",
                        column: x => x.RoleId,
                        principalTable: "GroupRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserInGroup_Group_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Group",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserInGroup_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GroupTask",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    GroupId = table.Column<Guid>(type: "uuid", nullable: false),
                    ParentId = table.Column<Guid>(type: "uuid", nullable: true),
                    Deadline = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    StatusId = table.Column<Guid>(type: "uuid", nullable: false),
                    HeadLine = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(5000)", maxLength: 5000, nullable: true),
                    ResponsibleId = table.Column<Guid>(type: "uuid", nullable: true),
                    ResponsibleUserId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupTask", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupTask_GroupTaskStatus_StatusId",
                        column: x => x.StatusId,
                        principalTable: "GroupTaskStatus",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GroupTask_GroupTask_ParentId",
                        column: x => x.ParentId,
                        principalTable: "GroupTask",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GroupTask_Group_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Group",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupTask_User_ResponsibleUserId",
                        column: x => x.ResponsibleUserId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "GroupChatParticipantLastRead",
                columns: table => new
                {
                    LastReadMessageId = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    GroupChatId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupChatParticipantLastRead", x => new { x.GroupChatId, x.UserId, x.LastReadMessageId });
                    table.ForeignKey(
                        name: "FK_GroupChatParticipantLastRead_GroupChatMessage_GroupChatId_L~",
                        columns: x => new { x.GroupChatId, x.LastReadMessageId },
                        principalTable: "GroupChatMessage",
                        principalColumns: new[] { "GroupChatId", "Ordinal" });
                    table.ForeignKey(
                        name: "FK_GroupChatParticipantLastRead_GroupChat_GroupChatId",
                        column: x => x.GroupChatId,
                        principalTable: "GroupChat",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GroupChatParticipantLastRead_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "GroupTaskUpdate",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: false),
                    TaskId = table.Column<Guid>(type: "uuid", nullable: false),
                    Content = table.Column<string>(type: "character varying(5000)", maxLength: 5000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupTaskUpdate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupTaskUpdate_GroupTask_TaskId",
                        column: x => x.TaskId,
                        principalTable: "GroupTask",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupTaskUpdate_User_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlockedUserInfo_PrevRoleId",
                table: "BlockedUserInfo",
                column: "PrevRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Group_DefaultRoleId",
                table: "Group",
                column: "DefaultRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupChat_GroupId",
                table: "GroupChat",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupChatMessage_SenderId",
                table: "GroupChatMessage",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupChatParticipant_UserId",
                table: "GroupChatParticipant",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupChatParticipantLastRead_GroupChatId_LastReadMessageId",
                table: "GroupChatParticipantLastRead",
                columns: new[] { "GroupChatId", "LastReadMessageId" });

            migrationBuilder.CreateIndex(
                name: "IX_GroupChatParticipantLastRead_UserId",
                table: "GroupChatParticipantLastRead",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupGroupRole_GroupsId",
                table: "GroupGroupRole",
                column: "GroupsId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupInvite_GroupId",
                table: "GroupInvite",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupInvite_ReceiverId",
                table: "GroupInvite",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupRole_GroupId",
                table: "GroupRole",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupTask_GroupId",
                table: "GroupTask",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupTask_ParentId",
                table: "GroupTask",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupTask_ResponsibleUserId",
                table: "GroupTask",
                column: "ResponsibleUserId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupTask_StatusId",
                table: "GroupTask",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupTaskStatus_GroupId",
                table: "GroupTaskStatus",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupTaskUpdate_CreatorId",
                table: "GroupTaskUpdate",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupTaskUpdate_TaskId",
                table: "GroupTaskUpdate",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Log_GroupId",
                table: "Log",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Log_InitiatorId",
                table: "Log",
                column: "InitiatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Log_LogActionId",
                table: "Log",
                column: "LogActionId");

            migrationBuilder.CreateIndex(
                name: "IX_Log_SubjectId",
                table: "Log",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalChat_User2Id",
                table: "PersonalChat",
                column: "User2Id");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalMessage_PersonalChatId",
                table: "PersonalMessage",
                column: "PersonalChatId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalMessage_SenderId",
                table: "PersonalMessage",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_User_SystemRoleId",
                table: "User",
                column: "SystemRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserInGroup_GroupId",
                table: "UserInGroup",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_UserInGroup_RoleId",
                table: "UserInGroup",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Group_GroupRole_DefaultRoleId",
                table: "Group",
                column: "DefaultRoleId",
                principalTable: "GroupRole",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Group_GroupRole_DefaultRoleId",
                table: "Group");

            migrationBuilder.DropTable(
                name: "BlockedUserInfo");

            migrationBuilder.DropTable(
                name: "GroupChatParticipant");

            migrationBuilder.DropTable(
                name: "GroupChatParticipantLastRead");

            migrationBuilder.DropTable(
                name: "GroupGroupRole");

            migrationBuilder.DropTable(
                name: "GroupInvite");

            migrationBuilder.DropTable(
                name: "GroupTaskUpdate");

            migrationBuilder.DropTable(
                name: "Log");

            migrationBuilder.DropTable(
                name: "PersonalMessage");

            migrationBuilder.DropTable(
                name: "UserInGroup");

            migrationBuilder.DropTable(
                name: "GroupChatMessage");

            migrationBuilder.DropTable(
                name: "GroupTask");

            migrationBuilder.DropTable(
                name: "LogAction");

            migrationBuilder.DropTable(
                name: "PersonalChat");

            migrationBuilder.DropTable(
                name: "GroupChat");

            migrationBuilder.DropTable(
                name: "GroupTaskStatus");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "SystemRole");

            migrationBuilder.DropTable(
                name: "GroupRole");

            migrationBuilder.DropTable(
                name: "Group");
        }
    }
}
