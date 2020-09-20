public class Customer {
    private String name;
    private List<Rental> rentals = new List<Rental> ();
    public Customer (String name) {
        this.name = name;
    }

    public String getName () {
        return name;
    }

    public void addRental (Rental rental) {
        rentals.Add (rental);
    }

    public String statement () {
        double totalAmount = 0;
        int frequentRenterPoints = 0;

        String result = "Rental record for " + getName () + "\n";
        foreach (var rental in rentals) {
            double amount = 0;
            switch (rental.getMovie ().getPriceCode ()) {
                case Movie.REGULAR:
                    amount += 2;
                    if (rental.getDaysRented () > 2)
                        amount += (rental.getDaysRented () - 2) * 1.5;
                    break;
                case Movie.NEW_RELEASE:
                    amount += rental.getDaysRented () * 3;
                    break;
                case Movie.CHILDREN:
                    amount += 1.5;
                    if (rental.getDaysRented () > 3)
                        amount += (rental.getDaysRented () - 3) * 1.5;
                    break;
            }
            // add frequent renter points
            frequentRenterPoints++;
            // add bonus for a two day new release rental
            if (rental.getMovie ().getPriceCode () == Movie.NEW_RELEASE && rental.getDaysRented () > 1)
                frequentRenterPoints++;

            // show figures for this rental
            result += "\t" + rental.getMovie ().getTitle () + "\t" + amount + "\n";

            totalAmount += amount;
        }
        result += "Amount owed is " + totalAmount + "\n";
        result += "You earned " + frequentRenterPoints + " frequent renter points";
        return result;
    }

}
public class Rental {
    private Movie movie;
    private int daysRented;

    public Rental (Movie movie, int daysRented) {
        this.movie = movie;
        this.daysRented = daysRented;
    }

    public Movie getMovie () {
        return movie;
    }

    public int getDaysRented () {
        return daysRented;
    }
}
public class Movie {
    public const int CHILDREN = 2;
    public const int REGULAR = 0;
    public const int NEW_RELEASE = 1;

    private String title;
    private int priceCode;

    public Movie (String title, int priceCode) {
        this.title = title;
        this.priceCode = priceCode;
    }

    public String getTitle () {
        return title;
    }

    public int getPriceCode () {
        return priceCode;
    }

    public void setPriceCode (int priceCode) {
        this.priceCode = priceCode;
    }