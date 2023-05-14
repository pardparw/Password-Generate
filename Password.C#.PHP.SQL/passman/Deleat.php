<?php
$servername = "localhost";
$username = "root";
$password = "password";
$dbname = "password_db";

$ID = $_POST["IDS"];

// Create connection
$conn = mysqli_connect($servername, $username, $password, $dbname);
// Check connection
if (!$conn) {
  die("Connection failed: " . mysqli_connect_error());
}

// sql to delete a record
$sql = "DELETE FROM passwords WHERE id='" . $ID ."' ";
$sql2 = "SET @autoid :=0";
$sql3 = "UPDATE passwords SET id = @autoid := (@autoid+1)";
$sql4 = "alter TABLE passwords Auto_increment = 1";

if (mysqli_query($conn, $sql)) {
  echo "Record deleted successfully";
} else {
  echo "Error deleting record: " . mysqli_error($conn);
}
    //Update List Id 
    if ($conn->query($sql2) === TRUE) {
        echo "Set1_OK<br>";
    } else {
        echo "SET_1Error: " . $sql2 . "<br>" . $conn->error;
    }
    if ($conn->query($sql3) === TRUE) {
        echo "Set2_OK<br>";
    } else {
        echo "SET2_Error: " . $sql3 . "<br>" . $conn->error;
    }
    
    if ($conn->query($sql4) === TRUE) {
        echo "Set3_OK<br>";
    } else {
        echo "Set3_Error: " . $sql4 . "<br>" . $conn->error;
    }
  
  

mysqli_close($conn);

?>