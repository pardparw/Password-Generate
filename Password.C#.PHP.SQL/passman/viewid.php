<?php
$servername = "localhost";
$username = "root";
$password = "password";
$dbname = "password_db";

$IDS = $_POST["IDS"];

// Create connection
$conn = new mysqli($servername, $username, $password, $dbname);
// Check connection
if ($conn->connect_error) {
  die("Connection failed: " . $conn->connect_error);
}

$sql = "SELECT site, username, password FROM passwords WHERE id='" . $IDS ."'";
//$sql = "SELECT id, firstname, lastname FROM MyGuests WHERE lastname='Doe'";
$result = $conn->query($sql);

if ($result->num_rows >= 0) {
  // output data of each row
  while($row = $result->fetch_assoc()) {
    echo $row["site"]. "," . $row["username"]. "," . $row["password"] . "";
  }
} else {
  echo "0 results";
}
$conn->close();
?>