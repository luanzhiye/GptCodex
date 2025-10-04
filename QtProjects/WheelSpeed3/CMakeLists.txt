cmake_minimum_required(VERSION 3.16)
project(WheelSpeedQML LANGUAGES CXX)

set(CMAKE_CXX_STANDARD 17)
set(CMAKE_CXX_STANDARD_REQUIRED ON)

set(CMAKE_AUTOMOC ON)
set(CMAKE_AUTORCC ON)
set(CMAKE_AUTOUIC ON)

find_package(QT NAMES Qt6 Qt5 REQUIRED COMPONENTS Quick Qml)
find_package(Qt${QT_VERSION_MAJOR} REQUIRED COMPONENTS Quick Qml)

set(PROJECT_SOURCES
    main.cpp
    qml.qrc
)

if(${QT_VERSION_MAJOR} GREATER_EQUAL 6)
    qt_add_executable(WheelSpeedQML
        ${PROJECT_SOURCES}
    )
else()
    add_executable(WheelSpeedQML
        ${PROJECT_SOURCES}
    )
endif()

target_link_libraries(WheelSpeedQML
    PRIVATE Qt${QT_VERSION_MAJOR}::Quick
            Qt${QT_VERSION_MAJOR}::Qml)

install(TARGETS WheelSpeedQML
    BUNDLE DESTINATION .
    LIBRARY DESTINATION ${CMAKE_INSTALL_LIBDIR})