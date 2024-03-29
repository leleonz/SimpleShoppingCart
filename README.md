# Simple Shopping Cart
This is to create a simple shopping cart system for a retail store selling clothes, e.g. T-shirts and Jeans. In this project only minimal design, feature and domain modelling will be considered, to satisfy the [focused use cases](#focused-use-cases).

## Focused Use Cases 
1. Able to add products into shopping cart.
2. Able to view total sum of products added in shopping cart.
3. Allow adding coupons for getting various discount (e.g. Buy X Free Y, Buy X sets and get Y% discount on Z set)

## Domain Models
### Entity
**Shopping Cart** 
: Each customer own a shopping cart. Serves as _Aggregate Root_ in this context.

| Public method | Description | Real life usage |
| ------------- | ----------- | --------------- |
| Add Items | Add selected product(s) into shopping cart | Call by ShoppingCartService (not included in project). ShoppingCartService will receive selected product(s) from either **API** call or **listening to domain event** raised (e.g. from Catalog service) |

### Value Object
**Cart Item** 
: Serve as record added into shopping cart. No public method exposed.

## Domain Modelling Concerns and Decision
### Domain Model Validation
**Problem:** How/ where should we validate and ensure valid domain model creation/ initialization?
There are **TWO** schools of thought: 
1. Domain model should always be valid - by ensuring validation is done before creation, e.g. using Factory pattern.
2. Domain model should ensure itself to be valid - it is the responsibility of domain model to do self validation, e.g. add validation logic in ctor.

**Decision:** 
- In this case, the chosen way is ✔️ 2. Domain model should ensure itself to be valid. 
- By extracting the validation logic in its own method and using base abstract class, _SelfValidationDomainModel.cs_ to enforce running _Validation_ method for every constructor calls.

### Shopping Cart Price Calculation
**Problem:** Is it the responsibility of shopping cart to calculate total price of added items/ products?

**Decision:** 
- To make this context simple, it is decided to have shopping cart to be responsibile for the price calculation. 
- In the future, if there are different formula of calculation, the calculation method can be extracted to Calculator interface. If such decision is being made, it might be a good idea to consider using Factory to create shopping cart with selected calculator class.

## Things to be improved
### Discount/ Coupon Setup
1. Introduced interface _IDiscountableCoupon_ with calculate discounted amount can be renamed as _IDiscountableItem_.
2. Ideally _Coupon_ should be an abstract class implementing _IDiscountableCoupon_. In the future there can be different kinds of coupon (e.g. BuyXFreeYCoupon, BuySetsGetXPercentDiscountCoupon, etc.) inherit this abstract class and override with different formula.
3. Add the omitted self validation check in all coupon classes.
4. Group discount or coupon into dedicated folder.
5. Current code will calculate coupon based on FIFO, adding a order/ priority field to coupon can be a good idea. Another way is to have the ability to calculate the lowest price for customers.
