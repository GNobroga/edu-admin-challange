using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduAdmin.Migrations
{
    /// <inheritdoc />
    public partial class PopulaBanco : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                INSERT INTO Users (Name, Email, Type) VALUES
                ('Alice Smith', 'alice.smith@example.com', 'TEACHER'),
                ('Bob Johnson', 'bob.johnson@example.com', 'STUDENT'),
                ('Charlie Brown', 'charlie.brown@example.com', 'TEACHER'),
                ('Diana Prince', 'diana.prince@example.com', 'STUDENT'),
                ('Edward Norton', 'edward.norton@example.com', 'TEACHER');
            """);

            migrationBuilder.Sql("""
               INSERT INTO Classes (Name) VALUES
                ('Class 1A'),
                ('Class 2B'),
                ('Class 3C'),
                ('Class 4D'),
                ('Class 5E');
            """);

            migrationBuilder.Sql("""
                INSERT INTO Subjects (Name, ClassId, TeacherId) VALUES
                ('Mathematics', 1, 1),  -- Associado à Class 1A e Teacher Alice Smith
                ('History', 2, 3),      -- Associado à Class 2B e Teacher Charlie Brown
                ('Science', 3, 1),      -- Associado à Class 3C e Teacher Alice Smith
                ('English', 4, 3),      -- Associado à Class 4D e Teacher Charlie Brown
                ('Art', 5, 5);          -- Associado à Class 5E e Teacher Edward Norton
            """);

            migrationBuilder.Sql("""
                INSERT INTO Grades (Name, Value, Date, StudentId, SubjectId) VALUES
                ('Math Exam 1', 85.5, '2023-05-20', 2, 1),   -- Associado ao estudante Bob Johnson e à disciplina Mathematics
                ('History Essay', 90.0, '2023-05-22', 4, 2), -- Associado ao estudante Diana Prince e à disciplina History
                ('Science Quiz', 78.0, '2023-06-15', 2, 3),  -- Associado ao estudante Bob Johnson e à disciplina Science
                ('English Test', 88.5, '2023-06-20', 4, 4),  -- Associado ao estudante Diana Prince e à disciplina English
                ('Art Project', 92.0, '2023-06-25', 2, 5);   -- Associado ao estudante Bob Johnson e à disciplina Art
            """);

            migrationBuilder.Sql("""
                -- Insere dados na tabela Attendances
            INSERT INTO Attendances (StudentId, Date, Present) VALUES
            (2, '2023-05-20', 1),   -- Estudante Bob Johnson presente em 20 de maio de 2023
            (4, '2023-05-22', 0),   -- Estudante Diana Prince ausente em 22 de maio de 2023
            (2, '2023-06-15', 1),   -- Estudante Bob Johnson presente em 15 de junho de 2023
            (4, '2023-06-20', 1),   -- Estudante Diana Prince presente em 20 de junho de 2023
            (2, '2023-06-25', 1);   -- Estudante Bob Johnson presente em 25 de junho de 2023
            """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE * FROM Users");
        }
    }
}
