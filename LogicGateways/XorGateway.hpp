#pragma once

#ifndef XorGateway_hpp
#define XorGateway_hpp

#include "LogicGateway.hpp"

class XorGateway :
    public LogicGateway
{
public:
    XorGateway();
    XorGateway(bool inputA, bool inputB);
    ~XorGateway();

    virtual bool getOutput() override;
};

#endif