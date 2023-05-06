#include <iostream>
#include <fstream>
#include <random>
#include "nlohmann/json.hpp"
#include <string.h>
#include "color.hpp"

using json = nlohmann::json;
using namespace std;
void savePasswords(const json& j, const std::string& filename)
{
    std::ofstream file(filename);
    file << std::setw(4) << j << std::endl;
}

json loadPasswords(const std::string& filename)
{
    std::ifstream file(filename);
    json j;
    file >> j;
    return j;
}

std::string generatePassword(int length)
{
    std::string password;
    std::string characters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*()_-+={}[]<>,.?/|";
    std::mt19937 rng(std::random_device{}());
    std::uniform_int_distribution<int> dist(0, characters.size() - 1);

    for (int i = 0; i < length; ++i) {
        password += characters[dist(rng)];
    }

    return password;
}

void addPassword(json& j, const std::string& site, const std::string& username, const std::string& password)
{
    json password_obj = {
        {"site", site},
        {"username", username},
        {"password", password}
    };
    j.push_back(password_obj);
    std::cout << "Password saved successfully." << std::endl;
}

void editPassword(json& j, int id, const std::string& site, const std::string& username, const std::string& password)
{
    if (id >= 0 && id < j.size()) {
        json& password_obj = j[id];
        password_obj["site"] = site;
        password_obj["username"] = username;
        password_obj["password"] = password;
        std::cout << "Password edited successfully." << std::endl;
    }
    else {
        std::cerr << "Error: Invalid password ID." << std::endl;
    }
}

void deletePassword(json& j, int id)
{
    if (id >= 0 && id < j.size()) {
        j.erase(j.begin() + id);
        std::cout << "Password deleted successfully." << std::endl;
    }
    else {
        std::cerr << "Error: Invalid password ID." << std::endl;
    }
}

void listPasswords(const json& j)
{   
    
    if (!j.empty()) {
        std::cout << "Saved passwords:" << std::endl;
        for (int i = 0; i < j.size(); ++i) {
            const auto& password = j[i];
            std::cout << "[" << i << "] " << password["site"].get<std::string>() << std::endl;
        }
        
    }
    else {
        std::cout << "No saved passwords found." << std::endl;
    }
}

void viewPassword(const json& j, int id)
{
    if (id >= 0 && id < j.size()) {
        const auto& password = j[id];
        std::cout << "Site: " << password["site"].get<std::string>() << std::endl;
        std::cout << "Username: " << password["username"].get<std::string>() << std::endl;
        std::cout << "Password: " << password["password"].get<std::string>() << std::endl;
    }
    else {
        std::cerr << "Error: Invalid password ID." << std::endl;
    }
}

int main()
{
    const std::string filename = "E:/Password/password/passwords.json";
    json passwords = loadPasswords(filename);
    string PasswordProgram;
    std::cout << "Welcome to Password Manager!" << std::endl;
    cout << "Input Password: ";
    cin >> PasswordProgram;
    if (PasswordProgram == "youpassword")
    {


        while (true) {
            std::cout << std::endl;
            std::cout << "Please choose an action:" << std::endl;
            std::cout << "1. Generate password" << std::endl;
            std::cout << "2. Add password" << std::endl;
            std::cout << "3. Edit password" << std::endl;
            std::cout << "4. Delete password" << std::endl;
            std::cout << "5. List passwords" << std::endl;
            std::cout << "6. View password" << std::endl;
            std::cout << "0. Quit" << std::endl;

            int choice;
            std::cout << "> ";
            std::cin >> choice;

            if (choice == 0) {
                break;
            }
            else if (choice == 1) {
                int length;
                std::cout << "Enter password length: ";
                std::cin >> length;
                std::string password = generatePassword(length);
                std::cout << "Generated password: " << password << std::endl;
            }
            else if (choice == 2) {
                std::string site, username, password;
                std::cout << "Enter site name: ";
                std::cin >> site;
                std::cout << "Enter username: ";
                std::cin >> username;
                std::cout << "Enter password: ";
                std::cin >> password;
                addPassword(passwords, site, username, password);
                savePasswords(passwords, filename);
            }
            else if (choice == 3) {
                int id;
                std::cout << "Enter password ID: ";
                std::cin >> id;
                std::string site, username, password;
                std::cout << "Enter site name: ";
                std::cin >> site;
                std::cout << "Enter username: ";
                std::cin >> username;
                std::cout << "Enter password: ";
                std::cin >> password;
                editPassword(passwords, id, site, username, password);
                savePasswords(passwords, filename);
            }
            else if (choice == 4) {
                int id;
                std::cout << "Enter password ID: ";
                std::cin >> id;
                deletePassword(passwords, id);
                savePasswords(passwords, filename);
            }
            else if (choice == 5) {
                listPasswords(passwords);
            }
            else if (choice == 6) {
                int id;
                std::cout << "Enter password ID: ";
                std::cin >> id;
                viewPassword(passwords, id);
            }
            else {
                std::cerr << "Error: Invalid choice." << std::endl;
            }
        }
    }
    else
    {
        cout << dye::red("wrong Password") << endl;
        cout << "Press [ctrl + C] to close this window . . .";
        cin >> passwords;
    }
    std::cout << "Goodbye!" << std::endl;

    return 0;
}

