#pragma once
#ifndef Button_hpp
#define Button_hpp

#include <SFML/Graphics.hpp>
#include <string.h>

class Button
{
private:
	sf::RectangleShape button;
	sf::Vector2f size;
	sf::Text text;
	sf::Color bgColor;

public:
	Button(sf::Vector2f size, sf::Vector2f position, std::string text);

	~Button();

	void setSize(sf::Vector2f newSize);
	void setPosition(sf::Vector2f position);
	void setText(std::string text);
	void setTextBackgroundColor(sf::Color bgColor);
	void setTextColor(sf::Color textColor);
	void setFont(sf::Font& font);
	void setTextSize(int size);
	void setButtonBackgroundColor(sf::Color color);
	void draw(sf::RenderWindow &window);
	bool isHovered(sf::RenderWindow& window);
};

#endif

