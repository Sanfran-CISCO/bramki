#pragma once

#ifndef NotGateway_hhp
#define NotGateway_hpp

#include "LogicGateway.hpp";

class NotGateway :
    public LogicGateway
{
public:
    NotGateway();
    NotGateway(bool input);
    ~NotGateway();

    virtual bool getOutput() override;
    void setInput(bool input);
};

#endif