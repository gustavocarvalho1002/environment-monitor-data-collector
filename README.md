# Real-Time Environment Monitor - Data Collector
**"Cloud-powered sensor tracking with Raspberry Pi technology."**

This repository contains the **Data Collector**, a key component of the Real-Time Environment Monitor solution. The project is composed of multiple applications working together to collect, process, and visualize environmental sensor data. This specific repository handles data collection on the Raspberry Pi.

## Overview
The **Data Collector** is a .NET Console Application running on the Raspberry Pi 5 with a Sense HAT. It captures sensor readings (e.g., temperature, humidity, pressure, and more) every 10-30 seconds and stores them locally in a SQLite database for further processing.

## Features
- Collects data from multiple Sense HAT sensors (gyroscope, accelerometer, magnetometer, temperature, pressure, humidity, etc.).
- Stores sensor readings in a local SQLite database.
- Configurable sampling interval (default: 10-30 seconds).

## Requirements
- **Raspberry Pi 5** with **Sense HAT**.
- .NET SDK (version X.X)
- SQLite database installed on the Raspberry Pi.

## How to Run
1. Clone this repository to your Raspberry Pi.
2. Navigate to the project directory.
3. Build and run the application:
    ```bash
    dotnet build
    dotnet run
    ```

## Configuration
You can configure the data sampling interval and other settings in the `appsettings.json` file located in the root of the project.
