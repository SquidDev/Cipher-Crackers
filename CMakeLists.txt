cmake_minimum_required (VERSION 2.6)
project (CipherCrackers)

set (project_SRC
	"src/main.cpp"
	
	"src/text/utilities.cpp"
)

set (CMAKE_CXX_FLAGS ${CMAKE_CXX_FLAGS} "-O3 -g -std=c++11 -Weverything -Werror -Wno-c++98-compat -Wno-c++98-compat-pedantic -Wno-missing-braces")

add_executable(CipherCrackers ${project_SRC})
