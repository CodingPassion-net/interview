public class Customer
{
    private String name;
    private List<Rental> rentals;
    public Customer(String name)
    {
        this.name = name;
        this.rentals = new List<Rental>();
    }

    public String getName()
    {
        return name;
    }

    public void addRental(Rental rental)
    {
        rentals.Add(rental);
    }

    public String statement()
    {
        StringBuilder statementBuilder = new StringBuilder($"Rental record for {getName()}\n");

        var statementElements = rentals.Select(rental =>
                                {
                                    return new
                                    {
                                        Amount = rental.GetAmount(),
                                        FrequentRenterPoints = new FrequentRenterPoints(rental).GetEarnedPoints(),
                                        Description = $"\t{rental.getMovie().getTitle()}\t{rental.GetAmount()}"
                                    };
                                });

        WriteOwedAmountTo(statementElements.Sum(statementElmenet => statementElmenet.Amount), statementBuilder);
        WriteFrequentRenterPointstTo(statementElements.Sum(statementElmenet => statementElmenet.FrequentRenterPoints), statementBuilder);

        return statementBuilder.ToString();
    }

    private void WriteOwedAmountTo(double ownedAmount, StringBuilder statement)
    {
        statement.Append($"Amount owed is " + ownedAmount + "\n");
    }
    private void WriteFrequentRenterPointstTo(double frequentRenterPoints, StringBuilder statement)
    {
        statement.Append($"You earned {frequentRenterPoints} frequent renter points");
    }

}

public class FrequentRenterPoints
{
    public Rental Rental { get; set; }
    public FrequentRenterPoints(Rental rental)
    {
        this.Rental = rental;
    }

    public int GetEarnedPoints()
    {
        if (EligibleForBonusPoints()) { return 2; }
        return 1;
    }

    public bool EligibleForBonusPoints()
    {
        return Rental.getMovie().GetMovieRentalStrategy() is MovieNewReleaseRentalStrategy &&
            Rental.getDaysRented() > 1;
    }

}

public class Movie
{

    private String title;
    private int priceCode;
    private MovieRentalStrategy movieRentalStrategy;

    public Movie(String title, int priceCode, MovieRentalStrategy movieRentalStrategy)
    {
        this.title = title;
        this.priceCode = priceCode;
        this.movieRentalStrategy = movieRentalStrategy;
    }

    public String getTitle()
    {
        return title;
    }
    public MovieRentalStrategy GetMovieRentalStrategy()
    {
        return movieRentalStrategy;
    }

    public int getPriceCode()
    {
        return priceCode;
    }

    public void setPriceCode(int priceCode)
    {
        this.priceCode = priceCode;
    }

    public MovieRentalStrategy getMovieRentalStrategy()
    {
        return movieRentalStrategy;
    }
}

public class Rental
{
    private Movie movie;
    private int daysRented;

    public Rental(Movie movie, int daysRented)
    {
        this.movie = movie;
        this.daysRented = daysRented;
    }
    public Movie getMovie()
    {
        return movie;
    }

    public double GetAmount()
    {
        return movie.GetMovieRentalStrategy().GetRentAmount(this);
    }
    public int getDaysRented()
    {
        return daysRented;
    }
}

public abstract class MovieRentalStrategy
{
    public abstract double GetRentAmount(Rental rental);
}

public class MovieRegularRentalStrategy : MovieRentalStrategy
{
    public override double GetRentAmount(Rental rental)
    {
        if (rental.getDaysRented() > 2) { return (rental.getDaysRented() - 2) * 1.5; }

        return 0.0;
    }
}

public class MovieNewReleaseRentalStrategy : MovieRentalStrategy
{
    public override double GetRentAmount(Rental rental)
    {
        return rental.getDaysRented() * 3;
    }
}

public class MovieChildrenRentalStrategy : MovieRentalStrategy
{
    private const double BASE_AMOUNT = 1.5;
    public override double GetRentAmount(Rental rental)
    {
        if (rental.getDaysRented() > 3) { return BASE_AMOUNT + ((rental.getDaysRented() - 3) * 1.5); }

        return 0.0;
    }
}