#pragma once

#ifndef AndGateway_hpp
#define AndGateway_hpp

#include "LogicGateway.hpp";

class AndGateway :
    public LogicGateway
{
public:
    AndGateway();
    AndGateway(bool inputA, bool inputB);
    ~AndGateway();

    bool getOutput() override;
};

#endif