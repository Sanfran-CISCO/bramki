#pragma once

#ifndef NotGateway_hhp
#define NotGateway_hpp

#include "LogicGateway.hpp";

class NotGateway :
    public LogicGateway
{
public:
    NotGateway();
    NotGateway(bool inputA, bool inputB);
    ~NotGateway();

    virtual bool getOutput() override;
};

#endif