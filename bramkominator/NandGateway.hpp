#pragma once

#ifndef NandGateway_hpp
#define NandGateway_hpp

#include "LogicGateway.hpp"

class NandGateway :
    public LogicGateway
{
public:
    NandGateway();
    NandGateway(bool inputA, bool inputB);
    ~NandGateway();

    bool getOutput() override;
};

#endif