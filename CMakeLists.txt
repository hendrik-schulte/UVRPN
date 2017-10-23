cmake_minimum_required(VERSION 2.8)
project(unityVRPN)

set(VRPN_BUILD_CLIENT_LIBRARY TRUE CACHE INTERNAL "" FORCE)

add_subdirectory(vrpn)

include_directories(vrpn)

add_library(unityVrpn SHARED native_source/unityVrpn.cpp)
target_link_libraries(unityVrpn vrpn)
