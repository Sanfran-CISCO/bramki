#include "Button.hpp"
#include <SFML/Graphics.hpp>
#include <string.h>

#define SFML_NO_DEPRECATED_WARNINGS

Button::Button(sf::Vector2f size, sf::Vector2f position, std::string text) {
	if (size.x > 0.0 && size.y > 0.0)
		this->button.setSize(size);
	
	if (position.x > 0.0 && position.y > 0.0) {
		this->button.setPosition(position);

		float textX = (position.x + this->button.getLocalBounds().width / 2) - (this->text.getLocalBounds().width / 2);
		float textY = (position.y + this->button.getLocalBounds().height / 2) - (this->text.getLocalBounds().height / 2);

		this->text.setPosition({ textX, textY });
	}

	if (text != "")
		this->text.setString(text);

	//this->text.setColor(sf::Color::Black);
	this->text.setFillColor(sf::Color::White);
	this->button.setFillColor(sf::Color::Magenta);
}

Button::~Button() {}

void Button::setSize(sf::Vector2f newSize) {
	if (newSize.x > 0.0 && newSize.y > 0.0)
		this->button.setSize(newSize);
}

void Button::setPosition(sf::Vector2f position) {
	if (position.x > 0.0 && position.y > 0.0) {
		this->button.setPosition(position);

		float textX = (position.x + this->button.getLocalBounds().width / 2) - (this->text.getLocalBounds().width / 2);
		float textY = (position.y + this->button.getLocalBounds().height / 2) - (this->text.getLocalBounds().height / 2);

		this->text.setPosition({ textX, textY });
	}
}


void Button::setText(std::string text) {
	if (text != "")
		this->text.setString(text);
}

void Button::setTextBackgroundColor(sf::Color bgColor) {
	this->text.setFillColor(bgColor);
}

void Button::setTextColor(sf::Color textColor) {
	//this->text.setColor(textColor);
}

void Button::setFont(sf::Font &font) {
	this->text.setFont(font);
}

void Button::setTextSize(int size) {
	if (size > 0)
		this->text.setCharacterSize(size);
}

void Button::setButtonBackgroundColor(sf::Color color) {
	this->button.setFillColor(color);
}

void Button::draw(sf::RenderWindow& window) {
	window.draw(this->button);
	window.draw(this->text);
}

bool Button::isHovered(sf::RenderWindow& window) {
	float mouseX = (float) sf::Mouse::getPosition(window).x;
	float mouseY = (float) sf::Mouse::getPosition(window).y;

	float buttonX = this->button.getPosition().x;
	float buttonY = this->button.getPosition().y;

	float buttonPositionWidth = this->button.getPosition().x + this->button.getLocalBounds().width;
	float buttonPositionHeight = this->button.getPosition().y + this->button.getLocalBounds().height;

	if (mouseX < buttonPositionWidth && mouseX > buttonX && mouseY < buttonPositionHeight && mouseY > buttonY)
		return true;

	return false;
}