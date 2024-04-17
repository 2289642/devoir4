using SQLite;
using Stagix.Models;
using System;
using System.Collections.Generic;

namespace Stagix
{
    public class PersonRepository
    {
        string _dbPath;

        public string StatusMessage { get; set; }

        private SQLiteConnection conn;

        private void Init()
        {
            if (conn != null)
                return;

            conn = new SQLiteConnection(_dbPath);
            conn.CreateTable<Person>();
        }

        public PersonRepository(string dbPath)
        {
            _dbPath = dbPath;
        }

        public void AddNewPerson(string num_DA, string pass)
        {
            int result = 0;
            try
            {
                Init();

                // basic validation to ensure a name was entered
                if (string.IsNullOrEmpty(num_DA))
                    throw new Exception("Valid DA required");

                // enter this line
                result = conn.Insert(new Person { numDA = num_DA, pwd = pass });

                StatusMessage = string.Format("{0} record(s) added (Name: {1})", result, num_DA);
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to add {0}. Error: {1}", num_DA, ex.Message);
            }

        }

        public List<Person> GetAllPeople()
        {
            try
            {
                Init();
                return conn.Table<Person>().ToList();
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
            }

            return new List<Person>();
        }

        public void DeletePerson(Person person)
        {
            try
            {
                Init();
                int result = conn.Delete(person);
                StatusMessage = $"{result} record(s) deleted successfully.";
            }
            catch (Exception ex)
            {
                StatusMessage = $"Failed to delete person. Error: {ex.Message}";
            }
        }

        public Person GetPersonById(int id)
        {
            try
            {
                Init();
                return conn.Table<Person>().FirstOrDefault(p => p.Id == id);
            }
            catch (Exception ex)
            {
                StatusMessage = $"Failed to retrieve person by ID {id}. {ex.Message}";
                return null;
            }
        }

        public void UpdatePerson(Person person)
        {
            try
            {
                Init();
                conn.Update(person);
                StatusMessage = $"Person with ID {person.Id} updated successfully.";
            }
            catch (Exception ex)
            {
                StatusMessage = $"Failed to update person. Error: {ex.Message}";
            }
        }
    }
}
