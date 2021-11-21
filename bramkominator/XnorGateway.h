#pragma once

#ifndef XnorGateway_hpp
#define XnorGateway_hpp

#include "LogicGateway.hpp"

class XnorGateway :
    public LogicGateway
{
public:
    XnorGateway();
    XnorGateway(bool inputA, bool inputB);
    ~XnorGateway();

    bool getOutput() override;
};

#endif