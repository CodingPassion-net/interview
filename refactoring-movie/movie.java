import java.util.ArrayList;
import java.util.List;

class Customer {

    private final String name;
    private final List<Rental> rentals = new ArrayList<Rental>();

    public Customer(final String name, List<Rental> rentals) {
        this.name = name;
        this.rentals = rentals;
    }

    public String getName() {
        return name;
    }

    public List<Rental> getRentals()
    {

    }

    //public void addRental(final Rental rental) {
      //  rentals.add(rental);
    //}
}

class CustomerStatementDomainService()
{
    Protected Customer customer;
    //private final List<Rental> rentals = new ArrayList<Rental>();

    public CustomerStatementDomainService(Customer customer) {
        this.customer = customer;
    }
}

interface calculateService
{
    public calculate();
}

class CustomerStatementCalculations implements calculateService
{
    private double totalAmount = 0;
    private int frequentRenterPoints = 0;
    private String result;
    Protected Customer customer;

    public CustomerStatementCalculations(Customer customer)
    {
        this.customer = customer;        
    }

    Protected execute()
    {
        this.initResult();
        this.calculate();
    }

    Protected initResult()
    {
        this.result = "Rental record for " + this.customer.getName() + "\n";
    }


    public calculate() {
        for (final Rental rental : this.customer.getRentals()) {
            double amount = 0;
            switch (rental.getMovie().getPriceCode()) {
                case Movie.REGULAR:
                    amount += 2;
                    if (rental.getDaysRented() > 2)
                        amount += (rental.getDaysRented() - 2) * 1.5;
                    break;
                case Movie.NEW_RELEASE:
                    amount += rental.getDaysRented() * 3;
                    break;
                case Movie.CHILDREN:
                    amount += 1.5;
                    if (rental.getDaysRented() > 3)
                        amount += (rental.getDaysRented() - 3) * 1.5;
                    break;
            }

            // add frequent renter points
            this.frequentRenterPoints++;
            // add bonus for a two day new release rental
            if (rental.getMovie().getPriceCode() == Movie.NEW_RELEASE && rental.getDaysRented() > 1)
                this.frequentRenterPoints++;

            // show figures for this rental
            

            totalAmount += amount;
        }

        result += "Amount owed is " + String.valueOf(totalAmount) + "\n";
        result += "You earned " + String.valueOf(frequentRenterPoints) + " frequent renter points";

        return result;
    }

    private addResultString()
    {
        this.result += "\t" + rental.getMovie().getTitle() + "\t" + String.valueOf(amount) + "\n";
    }

    public getResults()
    {
        return result;
    }
}

class Movie {
    public static final int CHILDREN = 2;
    public static final int REGULAR = 0;
    public static final int NEW_RELEASE = 1;

    private final String title;
    private int priceCode;

    public Movie(final String title, final int priceCode) {
        this.title = title;
        this.priceCode = priceCode;
    }

    public String getTitle() {
        return title;
    }

    public int getPriceCode() {
        return priceCode;
    }

    public void setPriceCode(final int priceCode) {
        this.priceCode = priceCode;
    }
}

class Rental {
    private final Movie movie;
    private final int daysRented;

    public Rental(final Movie movie, final int daysRented) {
        this.movie = movie;
        this.daysRented = daysRented;
    }

    public Movie getMovie() {
        return movie;
    }

    public int getDaysRented() {
        return daysRented;
    }
}