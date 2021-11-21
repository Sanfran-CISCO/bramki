#pragma once

#ifndef NorGateway_hpp
#define NorGateway_hpp

#include "LogicGateway.hpp"

class NorGateway :
    public LogicGateway
{
public:
    NorGateway();
    NorGateway(bool inputA, bool inputB);
    ~NorGateway();

    bool getOutput() override;
};

#endif