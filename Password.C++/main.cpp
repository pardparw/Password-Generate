#include <stdlib.h>
#include <iostream>
#include <random>
//mySql lib
#include "mysql_connection.h"
#include <cppconn/driver.h>
#include <cppconn/exception.h>
#include <cppconn/prepared_statement.h>

//color
#include "color.hpp"
using namespace std;

//for demonstration only. never save your password in the code!
const string server = "tcp://127.0.0.1:3306";
const string username = "root";
const string password = "password";

std::string generatePassword(int length)
{
    std::string password;
    std::string characters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&()_-+={}[]<>,.?/|";
    std::mt19937 rng(std::random_device{}());
    std::uniform_int_distribution<int> dist(0, characters.size() - 1);

    for (int i = 0; i < length; ++i) {
        password += characters[dist(rng)];
    }

    return password;
}



void addPassword(const std::string& site, const std::string& usernames, const std::string& passwords)
{
    //DB Setting
    sql::Driver* driver;
    sql::Connection* con;
    sql::Statement* stmt;
    sql::PreparedStatement* pstmt;

    try
    {
        driver = get_driver_instance();
        con = driver->connect(server, username, password);
    }
    catch (sql::SQLException e)
    {
        cout << "Could not connect to server. Error message: " << e.what() << endl;
        system("pause");
        exit(1);
    }
    //please create database "quickstartdb" ahead of time
    con->setSchema("password_db");
    
    pstmt = con->prepareStatement("INSERT INTO passwords(site, username, PASSWORD) VALUES(?,?,?)");
    pstmt->setString(1, site);
    pstmt->setString(2 , usernames);
    pstmt->setString(3, passwords);
    pstmt->execute();
    cout << "Add Sucess";
    
}

void editPassword(const std::string& id, const std::string& site, const std::string& usernames, const std::string& passwords)
{
    //DB Setting
    sql::Driver* driver;
    sql::Connection* con;
    sql::Statement* stmt;
    sql::PreparedStatement* pstmt;

    try
    {
        driver = get_driver_instance();
        con = driver->connect(server, username, password);
    }
    catch (sql::SQLException e)
    {
        cout << "Could not connect to server. Error message: " << e.what() << endl;
        system("pause");
        exit(1);
    }
    //please create database "quickstartdb" ahead of time
    con->setSchema("password_db");

    pstmt = con->prepareStatement("UPDATE passwords SET site = ?, username = ?, PASSWORD = ? WHERE id = " + id);
    
    pstmt->setString(1, site);
    pstmt->setString(2, usernames);
    pstmt->setString(3, passwords);
    pstmt->execute();
    cout << "Update Sucess";
}

void deletePassword(const std::string& id)
{
    sql::Driver* driver;
    sql::Connection* con;
    sql::PreparedStatement* pstmt;
    sql::ResultSet* result;

    try
    {
        driver = get_driver_instance();
        //for demonstration only. never save password in the code!
        con = driver->connect(server, username, password);
    }
    catch (sql::SQLException e)
    {
        cout << "Could not connect to server. Error message: " << e.what() << endl;
        system("pause");
        exit(1);
    }

    con->setSchema("password_db");

    //delete
    pstmt = con->prepareStatement("DELETE FROM passwords WHERE id = ?");
    pstmt->setString(1, id);
    result = pstmt->executeQuery();
    //Update ID
    pstmt = con->prepareStatement("SET @autoid :=0");
    pstmt->execute();
    pstmt = con->prepareStatement("UPDATE passwords SET id = @autoid := (@autoid+1)");
    pstmt->execute();
    pstmt = con->prepareStatement("alter TABLE passwords Auto_increment = 1");
    pstmt->execute();
    //clear Deleat data
    delete pstmt;
    delete con;
    delete result;
    cout << "Deleat Sucess" << endl;
}

void listPasswords()
{
    sql::Driver* driver;
    sql::Connection* con;
    sql::PreparedStatement* pstmt;
    sql::ResultSet* result;

    try
    {
        driver = get_driver_instance();
        //for demonstration only. never save password in the code!
        con = driver->connect(server, username, password);
    }
    catch (sql::SQLException e)
    {
        cout << "Could not connect to server. Error message: " << e.what() << endl;
        system("pause");
        exit(1);
    }

    con->setSchema("password_db");

    //select  
    pstmt = con->prepareStatement("SELECT * FROM passwords;");
    result = pstmt->executeQuery();
    cout << dye::white_on_black("Id|site|\t|Username|\t|Password|") << endl;
    while (result->next())
        cout << dye::black_on_white("[" + result->getString(1) + "] " + result->getString(2) + "\t" + result->getString(3) + "\t" + result->getString(4)) << endl;
        

    delete result;
    delete pstmt;
    delete con;
}

int main()
{
    //const std::string filename = "E:/Password/password/passwords.json";
   // json passwords = loadPasswords(filename);
    string PasswordProgram;
    std::cout << "Welcome to Password Manager!" << std::endl;
    cout << "Input Password: ";
    cin >> PasswordProgram;
    if (PasswordProgram == "password")
    {


        while (true) {
            std::cout << std::endl;
            std::cout << "Please choose an action:" << std::endl;
            std::cout << "1. Generate password" << std::endl;
            std::cout << "2. Add password" << std::endl;
            std::cout << "3. Edit password" << std::endl;
            std::cout << "4. Delete password" << std::endl;
            std::cout << "5. List passwords" << std::endl;
            std::cout << "0. Quit" << std::endl;

            int choice;
            std::cout << "> ";
            std::cin >> choice;
            

            if (choice == 0) {
                break;
            }
            else if (choice == 1) {//RandomPassword
                int length;
                std::cout << "Enter password length: ";
                std::cin >> length;
                std::string password = generatePassword(length);
                std::cout << "Generated password: " << password << std::endl;
            }
            else if (choice == 2) {//Add Password
                std::string site, username, password;
                std::cout << "Enter site name: ";
                std::cin >> site;
                std::cout << "Enter username: ";
                std::cin >> username;
                std::cout << "Enter password: ";
                std::cin >> password;
                addPassword(site, username, password);
            }
            else if (choice == 3) {//Edit Password
                string id;
                std::cout << "Enter password ID: ";
                std::cin >> id;
                std::string site, username, password;
                std::cout << "Enter site name: ";
                std::cin >> site;
                std::cout << "Enter username: ";
                std::cin >> username;
                std::cout << "Enter password: ";
                std::cin >> password;
                editPassword(id, site, username, password);
               
            }
            else if (choice == 4) {//Deleat Password
                string id;
                std::cout << "Enter password ID: ";
                std::cin >> id;
                deletePassword(id);
                
            }
            else if (choice == 5) {
                listPasswords();
            }
           
            else {
                std::cerr << "Error: Invalid choice." << std::endl;
            }
        }
    }
    else
    {
        cout << dye::red("wrong Password") << endl;
        cout << "Press [ctrl + C] to close this window . . ."<<endl;
        //cin >> passwords;
    }
    std::cout << "Goodbye!" << std::endl;

    return 0;
}


