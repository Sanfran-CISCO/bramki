#pragma once

#ifndef OrGateway_hpp
#define OrGateway_hpp

#include "LogicGateway.hpp"

class OrGateway :
    public LogicGateway
{
public:
    OrGateway();
    OrGateway(bool inputA, bool inputB);
    ~OrGateway();

    bool getOutput() override;
};

#endif