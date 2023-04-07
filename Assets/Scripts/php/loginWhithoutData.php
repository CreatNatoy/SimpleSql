<?php

$login = $_POST["login"];
$password = $_POST["password"];

$mysqli = new mysqli('*', '*', '*', '*');

if($mysqli->connection_error)
    die("Error connectiong to database: " . $mysqli->connection_error);

$query = "SELECT * FROM Users WHERE login = '$login'";

$result = $mysqli->query($query); 

if($result && $result->num_rows > 0){
    $row = $result->fetch_assoc(); 

    if(password_verify($password, $row["password"]))
        echo "Login successful";
    else
        echo "Password failed";
} else{
    echo "Login failed";
}

$mysqli->close(); 
?>