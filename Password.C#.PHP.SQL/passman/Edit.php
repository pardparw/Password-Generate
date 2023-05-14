<?php
$servername = "localhost";
$username = "root";
$password = "password";
$dbname = "password_db";

$ID = $_POST["IDS"];
$SiteName = $_POST["SiteName"];
$Username = $_POST["UserName"];
$Password = $_POST["Password"];

// Create connection
$conn = new mysqli($servername, $username, $password, $dbname);
// Check connection
if ($conn->connect_error) {
  die("Connection failed: " . $conn->connect_error);
}

$sql = "UPDATE passwords SET site = '". $SiteName ."', username = '" . $Username ."', password = '" . $Password . "' WHERE id='" . $ID ."'";

if ($conn->query($sql) === TRUE) {
  echo "Record updated successfully";
} else {
  echo "Error updating record: " . $conn->error;
}
$conn->close();
?>