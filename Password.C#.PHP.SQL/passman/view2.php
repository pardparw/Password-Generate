<?php
$servername = "localhost";
$username = "root";
$password = "password";
$dbname = "password_db";

// Create connection
$conn = new mysqli($servername, $username, $password, $dbname);
// Check connection
if ($conn->connect_error) {
  die("Connection failed: " . $conn->connect_error);
}

//echo "Connect Sucessfuly<br><br>";

$sql = "SELECT MAX(id) as `ids`  FROM `passwords`";
$res = mysqli_query($conn,$sql);
$data = mysqli_fetch_array($res);

echo $data['ids'];

$conn->close();
?>