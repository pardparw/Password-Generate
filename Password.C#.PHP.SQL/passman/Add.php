<?php
$servername = "localhost";
$username = "root";
$password = "password";
$dbname = "password_db";

//vaeiable submited bt user
$loginUser = $_POST["loginUser"];
$loginPassword = $_POST["loginPassword"];
$loginSite = $_POST["loginSite"];

// Create connection
$conn = new mysqli($servername, $username, $password, $dbname);
// Check connection
if ($conn->connect_error) {
  die("Connection failed: " . $conn->connect_error);
}

//echo "Connect Sucessfuly<br><br>";

$sql = "SELECT username FROM passwords WHERE username ='" . $loginUser . "'";
$result = $conn->query($sql);

if ($result->num_rows > 0) {
    echo "User is already taken";
} else {
  echo "Create user...";
  //Insert the user and password in to database
    $sql2 = "INSERT INTO passwords (site, username, password) VALUES ('" . $loginSite . "', '" . $loginUser . "', '" . $loginPassword . "')";
    if ($conn->query($sql2) === TRUE) {
        echo "New record created successfully";
      } else {
        echo "Error: " . $sql2 . "<br>" . $conn->error;
      }
}
$conn->close();
?>