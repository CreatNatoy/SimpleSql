<?php

$login = $_POST["login"];
$newData = $_POST["new_data"]

$mysqli = new mysqli('*', '*', '*', '*');

if($mysqli === false)
    die("Error: Could not connect. " . $mysqli->connect_error);

$query = "UPDATE Users SET data = '$newData' WHERE login = '$login'";
$result = $mysqli->query($query);

if($result)
    echo "Data updated successfully";
 else
    echo "Error updating data: " . $mysqli->error;

$mysqli->close();
?>