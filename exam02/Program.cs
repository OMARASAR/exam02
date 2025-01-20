using exam02;
using System;
using System.Collections.Generic;


class Program
{
    static void Main(string[] args)
    {
        Console.Write("Enter Subject Name: ");
        string subjectName = Console.ReadLine();
        Console.Write("Enter Subject ID: ");
        int subjectId = int.Parse(Console.ReadLine());

        Subject subject = new Subject(subjectId, subjectName);

        Console.WriteLine("Choose Exam Type:");
        Console.WriteLine("1. Final Exam");
        Console.WriteLine("2. Practical Exam");
        int examTypeChoice;
        Exam exam;

        do
        {
            Console.Write("Enter your choice (1 for Final, 2 for Practical): ");
            examTypeChoice = int.Parse(Console.ReadLine());

            if (examTypeChoice == 1)
            {
                exam = new FinalExam(0, 0); 
                break;
            }
            else if (examTypeChoice == 2)
            {
                exam = new PracticalExam(0, 0);
                break;
            }
            else
            {
                Console.WriteLine("Invalid choice. Please enter 1 or 2.");
            }
        } while (true);

        int examTime;
        do
        {
            Console.Write("Enter Exam Timer (between 30 and 120 minutes): ");
            examTime = int.Parse(Console.ReadLine());
            if (examTime < 30 || examTime > 120)
            {
                Console.WriteLine("Invalid input. Please enter a value between 30 and 120.");
            }
        } while (examTime < 30 || examTime > 120);

        exam.Time = examTime;

        Console.Write("Enter Number of Questions: ");
        int numberOfQuestions = int.Parse(Console.ReadLine());

        int totalMarks = 0; 

        for (int i = 0; i < numberOfQuestions; i++)
        {
            Console.WriteLine($"\nEnter details for Question {i + 1}:");
            Console.WriteLine("Choose Question Type:");
            Console.WriteLine("1. MCQ");
            Console.WriteLine("2. True/False");
            int questionType;

            do
            {
                Console.Write("Enter your choice (1 for MCQ, 2 for True/False): ");
                questionType = int.Parse(Console.ReadLine());

                if (questionType == 1)
                {
                    Console.Write("Enter Header: ");
                    string header = Console.ReadLine();
                    Console.Write("Enter Body: ");
                    string body = Console.ReadLine();
                    Console.Write("Enter Mark: ");
                    int mark = int.Parse(Console.ReadLine());
                    totalMarks += mark; 

                    MCQ mcq = new MCQ(header, body, mark);

                    Console.WriteLine("Enter 3 Choices:");
                    for (int j = 0; j < 3; j++)
                    {
                        Console.Write($"Choice {j + 1}: ");
                        mcq.AnswerList.Add(new Answer(j + 1, Console.ReadLine()));
                    }

                    Console.Write("Enter the index of the correct answer (1, 2, or 3): ");
                    mcq.RightAnswerId = int.Parse(Console.ReadLine());

                    exam.Questions.Add(mcq);
                    break;
                }
                else if (questionType == 2)
                {
                    Console.Write("Enter Header: ");
                    string header = Console.ReadLine();
                    Console.Write("Enter Body: ");
                    string body = Console.ReadLine();
                    Console.Write("Enter Mark: ");
                    int mark = int.Parse(Console.ReadLine());
                    totalMarks += mark; 

                    TrueOrFalse tf = new TrueOrFalse(header, body, mark);

                    tf.AnswerList.Add(new Answer(1, "True"));
                    tf.AnswerList.Add(new Answer(2, "False"));

                    Console.Write("Enter the correct answer (1 for True, 2 for False): ");
                    tf.RightAnswerId = int.Parse(Console.ReadLine());

                    exam.Questions.Add(tf);
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid choice. Please enter 1 or 2.");
                }
            } while (true);
        }

        subject.CreateExam(exam);

        DateTime startTime = DateTime.Now;

        Console.WriteLine($"\nStarting Exam for Subject: {subject.SubjectName}");
        int score = 0;

        foreach (var question in exam.Questions)
        {
            Console.WriteLine($"\n{question.Header}: {question.Body}");

            for (int i = 0; i < question.AnswerList.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {question.AnswerList[i].AnswerText}");
            }

            Console.Write("Enter your answer: ");
            int userAnswer = int.Parse(Console.ReadLine());

            if (userAnswer == question.RightAnswerId)
            {
                Console.WriteLine("Correct!");
                score += question.Mark; 
            }
            else
            {
                Console.WriteLine("Wrong!");
            }
        }

        DateTime endTime = DateTime.Now;

        TimeSpan timeTaken = endTime - startTime;

        Console.WriteLine($"\nExam Finished! Your Total Score: {score}/{totalMarks}");
        Console.WriteLine($"Time taken to complete the exam: {timeTaken.Minutes} minutes and {timeTaken.Seconds} seconds.");
    }
}
