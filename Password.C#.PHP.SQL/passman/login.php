<?php
$servername = "localhost";
$username = "root";
$password = "password";
$dbname = "password_db";

//vaeiable submited bt user

$loginPassword = $_POST["loginPassword"];


// Create connection
$conn = new mysqli($servername, $username, $password, $dbname);
// Check connection
if ($conn->connect_error) {
  die("Connection failed: " . $conn->connect_error);
}

//echo "Connect Sucessfuly<br><br>";

$sql = "SELECT password FROM user WHERE password ='" . $loginPassword . "'";
$result = $conn->query($sql);

if ($result->num_rows > 0) {
    echo "1";
} else {
  echo "0";
}
$conn->close();
?>